using System.Collections.Generic;
using vidly.Models;

namespace vidly.ViewModels
{
  public class RandomMovieViewModel
  {
    public Movie Movie { get; set; }

    public List<Customer> Customers { get; set; }
  }
}