using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication5.Filters
{
    public class TimeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ci = new CultureInfo("in-IN");
            var startTime = DateTime.Parse("12-17-2015 14:00:00", ci);
            var endTime = startTime.AddHours(2);
            if (DateTime.Now < startTime)
            {
                filterContext.Result = new RedirectToRouteResult(
           new RouteValueDictionary {{ "Controller", "Home" },
                                      { "Action", "Wait" } });
                base.OnActionExecuting(filterContext);
            }
            if (DateTime.Now > endTime)
            {
                filterContext.Result = new RedirectToRouteResult(
           new RouteValueDictionary {{ "Controller", "Home" },
                                      { "Action", "Thanks" } });
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
