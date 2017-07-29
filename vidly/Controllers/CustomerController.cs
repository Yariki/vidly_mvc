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
  public class CustomerController : Controller
  {
    private ApplicationDbContext _context;

    public CustomerController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      _context.Dispose();
    }
   
    // GET: Customer
    public ActionResult Index()
    {
      var customers = _context.Customers.Include("MembershipType");
      return View(new CustomersViewModel(){Customers = customers});
    }

    public ActionResult Details(int id)
    {
      var customer = _context.Customers.Include("MembershipType").FirstOrDefault(c => c.Id == id);
      if (customer == null)
      {
        return new HttpNotFoundResult();
      }
      return View(new CustomerViewModel(){Customer =  customer});
    }

    public ActionResult New()
    {
      return View();
    }

  }
}