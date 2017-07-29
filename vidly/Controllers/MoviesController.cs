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

    public ActionResult Details(int id)
    {
      var movie = new MovieViewModel() {Movie = _context.Movies.Include("Genre").FirstOrDefault(m => m.Id == id)};
      return View(movie);
    }

    // GET: Movies
    public ActionResult Random()
    {
      var movie = new Movie(){Id = 1,Name = "Shrek!"};

      var customers = new List<Customer>()
      {
        new Customer(){Name = "Customer 1"},
        new Customer(){Name = "Customer 2"}
      };

      var randomViewModel = new RandomMovieViewModel()
      {
        Movie = movie,
        Customers = customers
      };

      return View(randomViewModel);
    }

    [Route("movies/release/year/month:regex(\\d:{2}):range(1,12)")]
    public ActionResult ByReleaseDate(int year, int month)
    {
      return new EmptyResult();
    }

  }
}