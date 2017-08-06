using System.Collections.Generic;

namespace vidly.DTO
{
  public class NewRentalDto
  {
    public int CustomerId { get; set; }

    public List<int> MovieIds { get; set; }
  }
}