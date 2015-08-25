using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Web.Framework.Controllers;
using Inspiration.Core.Caching;
using Inspiration.Core.Data;

namespace Inspiration.Plugins.Person.Controllers
{
    public class PersonController : Inspiration.Web.Framework.Controller.SoianBaseController
    {
        public ICacheManager _cacheManager;
        public IDbContext _dbContext;
        public PersonController(ICacheManager cacheManager, IDbContext dbContext)
        {
            _cacheManager = cacheManager;
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var list = _dbContext.Set<Inspiration.Model.Person>().ToList();
            return View(list);
        }

    }
}