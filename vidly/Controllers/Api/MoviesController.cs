using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using vidly.DAL;
using vidly.DTO;
using vidly.Helpers;
using vidly.Models;

namespace vidly.Controllers.Api
{
  public class MoviesController : ApiController
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

    [HttpGet]
    public IHttpActionResult GetMovies(string query = null)
    {

      IQueryable<Movie> movies = _context.Movies
        .Include("Genre")
        .Where(m => m.NumberAvailable > 0);

      if (!string.IsNullOrWhiteSpace(query))
      {
        movies = movies.Where(m => m.Name.Contains(query));
      }

      return Ok(movies.ToList().Select(Mapper.Map<Movie,MovieDto>));
    }


    [HttpGet]
    public IHttpActionResult GetMovie(int id)
    {
      var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
      if (movie == null)
      {
        return NotFound();
      }
      return Ok(Mapper.Map<Movie, MovieDto>(movie));
    }

    [Authorize(Roles = RoleNames.CanManageMovies)]
    [HttpPost]
    public IHttpActionResult CreateMovie(MovieDto movieDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      var movie = Mapper.Map<MovieDto, Movie>(movieDto);
      _context.Movies.Add(movie);
      _context.SaveChanges();
      movieDto.Id = movie.Id;
      return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
    }

    [HttpPut]
    [Authorize(Roles = RoleNames.CanManageMovies)]
    public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
      if (movie == null)
      {
        return NotFound();
      }
      Mapper.Map(movieDto, movie);
      _context.SaveChanges();

      return Ok(movieDto);
    }

    [HttpDelete]
    [Authorize(Roles = RoleNames.CanManageMovies)]
    public IHttpActionResult DeleteMovie(int id)
    {
      var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
      if (movie == null)
      {
        return NotFound();
      }
      _context.Movies.Remove(movie);
      _context.SaveChanges();
      return Ok();
    }


  }
}