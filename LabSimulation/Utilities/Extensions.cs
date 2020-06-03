using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabSimulation.Utilities
{
    public static class Extensions
    {
        public static string IsActive(this HtmlHelper html,
                                  string control)
        {
            var routeData = html.ViewContext.RouteData;

            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control == routeControl;
            return returnActive ? "ActiveLink" : "";
        }
    }
}