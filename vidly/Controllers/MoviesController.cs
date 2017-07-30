using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly.DAL;
using vidly.Models;
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
      var movies = new MoviesViewModel(){Movies = _context.Movies.Include("Genre")};
      return View(movies);
    }

    public ActionResult New()
    {
      var viewModel = new MovieFormViewModel()
      {
        Genres = _context.Genres
      };
      return View("MoviewForm",viewModel);
    }


    public ActionResult Details(int id)
    {
      var movie = new MovieFormViewModel() {Movie = _context.Movies.Include("Genre").FirstOrDefault(m => m.Id == id)};
      return View(movie);
    }
    
    [Route("movies/release/year/month:regex(\\d:{2}):range(1,12)")]
    public ActionResult ByReleaseDate(int year, int month)
    {
      return new EmptyResult();
    }

    public ActionResult Save(MovieFormViewModel movieForm)
    {
      if (movieForm.Movie.Id == 0)
      {
        _context.Movies.Add(movieForm.Movie);
      }
      else
      {
        var moviewInDb = _context.Movies.SingleOrDefault(m => m.Id == movieForm.Movie.Id);
        moviewInDb.Name = movieForm.Movie.Name;
        moviewInDb.AddedDate = movieForm.Movie.AddedDate;
        moviewInDb.ReleaseDate = movieForm.Movie.ReleaseDate;
        moviewInDb.GenreId = movieForm.Movie.GenreId;
      }
      _context.SaveChanges();


      return RedirectToAction("Index", "Movies");
    }

    public ActionResult Edit(int id)
    {
      var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

      var moviewViewModel = new MovieFormViewModel()
      {
        Movie =  movie,
        Genres = _context.Genres
      };
      return View("MoviewForm", moviewViewModel);
    }
  }
}