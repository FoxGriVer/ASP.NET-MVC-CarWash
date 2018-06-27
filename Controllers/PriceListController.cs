using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarWash.Models;

namespace CarWash.Controllers
{
    public class PriceListController : Controller
    {
        CarWashModelContainer db = new CarWashModelContainer();

        // GET: PriceList
        public ActionResult Index()
        {
            IEnumerable < Services > servicesList = db.ServicesSet;
            return View(servicesList);
        }

        public int ConnectServicesAndCategories(int code, int category)
        {
            ServicesByCategorySet servicesByCategory = db.ServicesByCategorySet.FirstOrDefault(x => x.ServicesCodeService == code && x.CategoryOfCarCategoryId == category);
            return servicesByCategory.Price;
        }
                
    }
}