using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vidly.DAL;
using vidly.DTO;
using vidly.Models;

namespace vidly.Controllers.Api
{
  public class NewRentalController : ApiController
  {
    private ApplicationDbContext _context;

    public NewRentalController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      _context.Dispose();
    }


    [HttpPost]
    public IHttpActionResult CreateNewRental(NewRentalDto model)
    {

      var customer = _context.Customers.Single(c => c.Id == model.CustomerId);

      var movies = _context.Movies.Where(m => model.MovieIds.Contains(m.Id));

      foreach (var movie in movies)
      {
        if (movie.NumberAvailable == 0)
        {
          return BadRequest($"The movie '{movie.Name}' ius not available.");
        }

        movie.NumberAvailable--;
        var newRental = new Rental()
        {
          Customer = customer,
          Movie = movie,
          DateRented = DateTime.Now
        };
        _context.Rentals.Add(newRental);
      }
      _context.SaveChanges();


      return Ok();
    }
  }
}
