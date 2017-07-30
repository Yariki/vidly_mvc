﻿using System;
using System.ComponentModel.DataAnnotations;

namespace vidly.Models
{
  public class Movie
  {
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public DateTime AddedDate { get; set; }

    public Genre Genre { get; set; }

    [Required]
    public int GenreId { get; set; }

    [Required]
    [Range(1, 20)]
    public int NumberInStock { get; set; }
  }
}