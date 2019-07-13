using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShortenUrlWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (UrlModel entity = new UrlModel())
            {
                string shortName = RouteData.Values["id"]?.ToString();
                if (string.IsNullOrEmpty(shortName))
                {
                    return View(entity.UrlInfo.ToList());
                }
                else
                {
                    var urlInfo = entity.UrlInfo.FirstOrDefault(f => f.ShortName == shortName);
                    if (urlInfo != null)
                    {
                        return Redirect(urlInfo.Url);
                    }
                    else
                    {
                        return View("Error404");
                    }
                }
            }
        }
    }
}