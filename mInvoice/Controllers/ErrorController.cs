using System.Web.Mvc;

namespace mInvoice.Controllers
{
    public class ErrorController : BaseController
    {
        public ViewResult Index()
        {
            return View("Error");
        }
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }
    }
}