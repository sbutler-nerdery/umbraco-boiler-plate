using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Examine;
using Examine.LuceneEngine.SearchCriteria;
using Examine.SearchCriteria;

using Umbraco.Web.Mvc;

namespace Umbraco.Boilerplate.Web.Controllers
{
    public class SearchController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Results()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["query"]))    
            {
                var term = Request.QueryString["query"];
                var searchProvider = ExamineManager.Instance.SearchProviderCollection["ExternalSearcher"];
                var criteria = searchProvider.CreateSearchCriteria(UmbracoExamine.IndexTypes.Content);
                var filter = criteria.GroupedOr(
                    new[] { "nodeName", "mainContent" }, new [] { term.MultipleCharacterWildcard() })
                    .Not()
                    .Field("showInNavigation", "0")
                    .Compile();

                var results = searchProvider.Search(filter);

                if(results != null)
                {
                    return View(results);
                }
            }

            return View(new List<SearchResult>());
        }
    }
}
