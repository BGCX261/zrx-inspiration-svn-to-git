using System.Web.Mvc;
using System.Web.Routing;
using Inspiration.Web.Framework.Mvc.Routes;

namespace Inspiration.Plugins.Person
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Person",
                 "Plugins/Person",
                 new { controller = "Person", action = "Index" },
                 new[] { "Inspiration.Plugins.Person.Controllers" }
            );
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
