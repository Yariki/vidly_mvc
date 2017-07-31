using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using vidly.DAL;
using vidly.DTO;
using vidly.Models;

namespace vidly.Controllers.Api
{
  public class CustomersController : ApiController
  {
    private ApplicationDbContext _context;

    public CustomersController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      _context.Dispose();
    }

    // GET /api/customers
    [HttpGet]
    public IHttpActionResult GetCustomers()
    {
      return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
    }

    // GET /api/customers/1
    [HttpGet]
    public IHttpActionResult GetCustomer(int id)
    {
      var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
      if (customer == null)
        return NotFound();
      return Ok(Mapper.Map<Customer,CustomerDto>(customer));
    }

    // POST /api/customers
    [HttpPost]
    public IHttpActionResult CreateCustomer(CustomerDto customerDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest("Not Valid");
      }

      var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
      _context.Customers.Add(customer);
      _context.SaveChanges();
      customerDto.Id = customer.Id;
      return Created(new Uri($"{Request.RequestUri}/{customer.Id}"), customerDto);
    }

    // PUT /api/customers/1
    [HttpPut]
    public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
      if (customerInDb == null)
      {
        return NotFound();
      }
      Mapper.Map(customerDto, customerInDb);
      _context.SaveChanges();
      return Ok(customerDto);
    }

    // DELETE /api/customers/1
    [HttpDelete]
    public IHttpActionResult DeleteCustomer(int id)
    {
      var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
      if (customerInDb == null)
      {
        return NotFound();
      }
      _context.Customers.Remove(customerInDb);
      _context.SaveChanges();
      return Ok();
    }


  }
}