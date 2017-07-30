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
      var membershipTypes = _context.MembershipTypes;
      var viewModel = new CustomerFormViewModel()
      {
        Customer =  new Customer(),
        MembershipTypes = membershipTypes
      };

      return View("CustomerForm",viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save(CustomerFormViewModel customerForm)
    {
      if (!ModelState.IsValid)
      {
        var formViewModel = new CustomerFormViewModel
        {
          Customer = customerForm.Customer,
          MembershipTypes = _context.MembershipTypes
        };
        return View("CustomerForm", formViewModel);
      }
      
      if (customerForm.Customer.Id == 0)
      {
        _context.Customers.Add(customerForm.Customer);
      }
      else
      {
        var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customerForm.Customer.Id);
        if (customerInDb != null)
        {
          customerInDb.Name = customerForm.Customer.Name;
          customerInDb.Birthday = customerForm.Customer.Birthday;
          customerInDb.IsSubscribedToNewsLetter = customerForm.Customer.IsSubscribedToNewsLetter;
          customerInDb.MembershipTypeId = customerForm.Customer.MembershipTypeId;
        }
      }
      _context.SaveChanges();

      return RedirectToAction("Index","Customer");
    }

    public ActionResult Edit(int id)
    {
      var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
      var editViewModel = new CustomerFormViewModel()
      {
        Customer =  customer,
        MembershipTypes =  _context.MembershipTypes.ToList()
      };
      return View("CustomerForm", editViewModel);
    }
  }
}