using System;
using System.Linq;
using System.Web.Mvc;
using vidly.DAL;
using vidly.Helpers;
using vidly.ViewModels;

namespace vidly.Controllers
{
  public class MoviesController : Controller
  {
    private ApplicationDbContext _context;

    public MoviesController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      _context.Dispose();
    }

    public ActionResult Index()
    {
      var movies = new MoviesViewModel() { Movies = _context.Movies.Include("Genre") };

      return View(User.IsInRole(RoleNames.CanManageMovies) ? "List" : "ReadOnlyList", movies);
    }

    [Authorize(Roles = RoleNames.CanManageMovies)]
    public ActionResult New()
    {
      var viewModel = new MovieFormViewModel()
      {
        Genres = _context.Genres
      };
      return View("MoviewForm", viewModel);
    }

    public ActionResult Details(int id)
    {
      var movie = new MovieFormViewModel(_context.Movies.Include("Genre").FirstOrDefault(m => m.Id == id))
      {
        Genres = _context.Genres
      };
      return View(movie);
    }

    [Route("movies/release/year/month:regex(\\d:{2}):range(1,12)")]
    public ActionResult ByReleaseDate(int year, int month)
    {
      return new EmptyResult();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save(MovieFormViewModel movieForm)
    {
      if (!ModelState.IsValid)
      {
        var viewModel = new MovieFormViewModel(movieForm.GetMovie())
        {
          Genres = _context.Genres
        };
        return View("MoviewForm", viewModel);
      }

      if (movieForm.Id == 0)
      {
        var movie = movieForm.GetMovie();
        movie.AddedDate = DateTime.Now;
        _context.Movies.Add(movie);
      }
      else
      {
        var moviewInDb = _context.Movies.SingleOrDefault(m => m.Id == movieForm.Id);
        moviewInDb.Name = movieForm.Name;
        moviewInDb.ReleaseDate = movieForm.ReleaseDate.Value;
        moviewInDb.GenreId = movieForm.GenreId.Value;
        moviewInDb.NumberInStock = movieForm.NumberInStock.Value;
      }
      _context.SaveChanges();

      return RedirectToAction("Index", "Movies");
    }

    [Authorize(Roles = RoleNames.CanManageMovies)]
    public ActionResult Edit(int id)
    {
      var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

      var moviewViewModel = new MovieFormViewModel(movie)
      {
        Genres = _context.Genres
      };
      return View("MoviewForm", moviewViewModel);
    }
  }
}