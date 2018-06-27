using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarWash.Models;

namespace CarWash.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            //using(var db = new CarWashModelContainer())
            //{
            //    var Client1 = db.ClientSet.Create();
            //    var Car1 = db.CarClientSet.Create();

            //    Client1.Name = "Pavel";
            //    Client1.Surname = "Smirnov";
            //    Client1.PhoneNumer = 8800;

            //    Car1.Mark = "Toyota";
            //    Car1.Model = "Camry";

            //    Client1.CarClient.Add(Car1);
            //    Car1.Client.Add(Client1);

            //    db.ClientSet.Add(Client1);
            //    db.CarClientSet.Add(Car1);
            //    db.SaveChanges();                             
            //}
            return View();
        }
    }
}