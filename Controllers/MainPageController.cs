using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWash.Controllers
{
    public class MainPageController : Controller
    {
        //Все тестовые логины try10@yandex.ru
        //а пароли            Trytrytry10!
        //меняются только цифры


        // GET: MainPage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveRecord()
        {
            return View();
        }
    }
}