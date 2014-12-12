using System.Web;
using System.Web.Mvc;

namespace Demo1.Infrastructure
{
    public class AnalyticsFilter : ActionFilterAttribute
    {
        private readonly string _description;

        public AnalyticsFilter(string description)
        {
            _description = description;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Segment.Analytics.Client.Track(HttpContext.Current.User.Identity.GetUserEmail(), _description);
            base.OnActionExecuting(filterContext);
        }
    }
}