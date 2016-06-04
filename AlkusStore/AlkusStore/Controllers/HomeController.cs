using System.Web.Mvc;

namespace AlkusStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List", "Products");
        }
    }
}