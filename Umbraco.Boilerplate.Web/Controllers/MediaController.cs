using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Boilerplate.Web.ViewModels;
using Umbraco.Core;
using Umbraco.Web.Mvc;

namespace Umbraco.Boilerplate.Web.Controllers
{
    public class MediaController : SurfaceController
    {

        [ChildActionOnly]
        public ActionResult Carousel(int folderId, int width)
        {
            var service = ApplicationContext.Current.Services.MediaService;
            var items = service.GetChildren(folderId).Select(x => 
                new CarouselItem { 
                    Title = x.GetValue("title").ToString(),
                    Description = x.GetValue("description").ToString(),
                    Url = x.GetValue("umbracoFile").ToString()
                }).ToList();

            ViewBag.Width = width;

            return View(items);
        }
    }
}
