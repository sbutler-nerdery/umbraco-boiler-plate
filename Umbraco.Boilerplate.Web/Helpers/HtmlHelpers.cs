using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace Umbraco.Boilerplate.Web.Helpers
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Find out if the specified Umbraco page node is visible for the current user.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageNode"></param>
        /// <returns></returns>
        public static bool IsNodeVisible(this HtmlHelper html, int pageId, string pagePath)
        {
            var isVisible = true;
            if (umbraco.library.IsProtected(pageId, pagePath))
            {
                isVisible = umbraco.library.HasAccess(pageId, pagePath);
            }
            return isVisible;
        }
    }
}