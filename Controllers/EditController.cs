using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShortenUrlWebApplication.Controllers
{
    public class ActionsController : Controller
    {
        public ActionResult Edit(int id)
        {
            using (UrlModel entity = new UrlModel())
            {
                var urlInfo = entity.UrlInfo.FirstOrDefault(f => f.Id == id);
                return View(urlInfo ?? new UrlInfo() { Id = 0 });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UrlInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (UrlModel entity = new UrlModel())
            {
                if(entity.UrlInfo.Any(a => a.ShortName == model.ShortName && a.Id != model.Id))
                {
                    ModelState.AddModelError("ShortName", "Short name already in used.");
                    return View(model);
                }
                if (model.Id != 0)
                {
                    var urlInfo = entity.UrlInfo.FirstOrDefault(f => f.Id == model.Id);
                    if (urlInfo != null)
                    {
                        urlInfo.ShortName = model.ShortName;
                        urlInfo.Url = model.Url;
                    }
                }
                else
                {
                    entity.UrlInfo.Add(model);
                }
                await entity.SaveChangesAsync();
                return Redirect("/");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            using (UrlModel entity = new UrlModel())
            {
                var urlInfo = entity.UrlInfo.FirstOrDefault(f => f.Id == id);
                if(urlInfo != null)
                {
                    entity.UrlInfo.Remove(urlInfo);
                    await entity.SaveChangesAsync();
                }
                return Redirect("/");
            }
        }
        
    }
}
