using System.Collections.Generic;
using vidly.Models;

namespace vidly.ViewModels
{
  public class CustomersViewModel
  {
    public IEnumerable<Customer> Customers { get; set; }
  }
}