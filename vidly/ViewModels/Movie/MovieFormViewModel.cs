using System.Collections.Generic;
using vidly.Models;

namespace vidly.ViewModels
{
  public class MovieFormViewModel
  {
    public Movie Movie { get; set; }

    public IEnumerable<Genre> Genres { get; set; }

  }
}