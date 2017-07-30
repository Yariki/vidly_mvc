using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using vidly.Models;

namespace vidly.ViewModels
{
  public class MovieFormViewModel
  {
    public MovieFormViewModel()
    {
      Id = 0;
    }

    public MovieFormViewModel(Movie movie)
    {
      Id = movie.Id;
      Name = movie.Name;
      ReleaseDate = movie.ReleaseDate;
      GenreId = movie.GenreId;
      NumberInStock = movie.NumberInStock;
    }

    public IEnumerable<Genre> Genres { get; set; }

    public int? Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    public DateTime? ReleaseDate { get; set; }

    [Required]
    public int? GenreId { get; set; }

    [Required]
    [Range(1, 20)]
    public int? NumberInStock { get; set; }

    public string Title
    {
      get { return Id != 0 ? "Edit Movie" : "New Movie"; }
    }

    public Movie GetMovie()
    {
      return new Movie()
      {
        Id = this.Id.Value,
        Name = this.Name,
        ReleaseDate = this.ReleaseDate.Value,
        GenreId = this.GenreId.Value,
        NumberInStock = this.NumberInStock.Value
      };
    }
  }
}