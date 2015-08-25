using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspiration.DataAccess;
using Inspiration.Web.Framework.Controller;
using Inspiration.Core.Caching;
using Inspiration.Core.Data;
using Inspiration.Model;

namespace Inspiration.MvcWeb.Controllers
{
    public class HomeController : SoianBaseController
    {
        public ICacheManager _cacheManager;
        public IDbContext _dbContext;
        public HomeController(ICacheManager cacheManager, IDbContext dbContext)
        {
            _cacheManager = cacheManager;
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            _dbContext.Set<User>().Count();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
