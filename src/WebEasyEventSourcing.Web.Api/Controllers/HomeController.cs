using System.Web.Mvc;

namespace WebEasyEventSourcing.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger/ui/index");
        }
    }
}