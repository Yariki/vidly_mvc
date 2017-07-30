using System.Web.Http;
using vidly.DAL;

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
  }
}