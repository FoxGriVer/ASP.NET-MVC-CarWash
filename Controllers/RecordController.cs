using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarWash.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Globalization;

namespace CarWash.Controllers
{
    public class RecordController : Controller
    {
        private ApplicationUserManager _userManager;
        CarWashModelContainer db = new CarWashModelContainer();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CreateAuthenticatedRecord()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentAppClient = UserManager.Users.FirstOrDefault(x => x.Id == userId);

            Client client = db.ClientSet.FirstOrDefault(x => x.ClientId == currentAppClient.ClientGuid);
            List<CarClientSet> listClientCars = db.CarClientSet.Where(x => x.ClientClientId == currentAppClient.ClientGuid).ToList();

            var recordModel = new AutorizedUserRecord();
            recordModel.ClientGuid = client.ClientId;
            recordModel.Name = client.Name;
            recordModel.Surname = client.Surname;
            recordModel.Patronymic = client.Patronymic;
            recordModel.Phone = client.PhoneNumer;
            recordModel.UserCars = new List<Car>();
            foreach (var item in listClientCars)
            {
                Car userCar = db.CarSet.FirstOrDefault(x => x.CarId == item.CarCarId);
                recordModel.UserCars.Add(userCar);
            }
                
            return View(recordModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthenticatedRecord(AutorizedUserRecord model)
        {
            var pickedCar = db.CarClientSet.FirstOrDefault(x => x.CarCarId == model.CurrentCar.CarId);

            var newRecord = db.RecordSet.Create();
            newRecord.RecordId = Guid.NewGuid();
            newRecord.CarClientCarNumber = pickedCar.CarNumber;
            if(pickedCar.PriorityBox != null && pickedCar.PriorityCarWasher != null)
            {
                newRecord.PriorityWasher = db.CarWasherSet.FirstOrDefault(x => x.CarWasherId == pickedCar.PriorityCarWasher).Surname;
                newRecord.PriorityBox = pickedCar.PriorityBox;
            }            
            if(newRecord.PriorityBox != null && newRecord.PriorityWasher != null)
            {
                newRecord.BoxesBoxId = pickedCar.PriorityBox;
                newRecord.CarWasherCarWasherId = pickedCar.PriorityCarWasher;
            }
            else
            {
                newRecord.BoxesBoxId = db.BoxesSet.FirstOrDefault(x => x.Description == "Default").BoxId;
                newRecord.CarWasherCarWasherId = db.CarWasherSet.FirstOrDefault(x => x.Surname == "Default" && x.Name == "Default").CarWasherId;
            }            
            newRecord.DateTime = Convert.ToDateTime(model.DateRecord.Month + "/" + model.DateRecord.Day + "/" +
                    model.DateRecord.Year + " " + model.TimeRecord.Hour + ":" + model.TimeRecord.Minute + ":" + model.TimeRecord.Second,
                    CultureInfo.InvariantCulture);
            db.RecordSet.Add(newRecord);
            db.SaveChanges();
            var newOrder = db.OrderSet.Create();
            newOrder.OrderId = Guid.NewGuid();
            newOrder.RecordRecordId = newRecord.RecordId;
            db.OrderSet.Add(newOrder);
            db.SaveChanges();
            return RedirectToAction("ShowRecordList", newRecord);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddServicesToRecord(RecordListViewModel model)
        {
            var newRecordViewModel = new RecordListViewModel {
                DateTime = model.DateTime,
                PriorityBox = db.BoxesSet.FirstOrDefault(x => x.BoxId == model.PriorityBox.BoxId),
                PriorityCarWasher = db.CarWasherSet.FirstOrDefault(x => x.CarWasherId == model.PriorityCarWasher.CarWasherId),
                CurrentCar = db.CarSet.FirstOrDefault(x => x.Model == model.CurrentCar.Model),
                RecordGuid = model.RecordGuid };
            newRecordViewModel.ListOfServices = db.ServicesSet.ToList();
            return View(newRecordViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RefreshServicesToRecord(RecordListViewModel model)
        {
            var currentCar = db.CarSet.FirstOrDefault(m => m.Model == model.CurrentCar.Model);
            var serviceToRecord = db.ServicesByCategorySet.FirstOrDefault(x => x.ServicesCodeService == model.PickedService.CodeService &&
            x.CategoryOfCarCategoryId == currentCar.CategoryOfCarCategoryId);

            var currentOrder = db.OrderSet.FirstOrDefault(x => x.RecordRecordId == model.RecordGuid);

            serviceToRecord.OrderSet.Add(currentOrder);
            currentOrder.ServicesByCategorySet1.Add(serviceToRecord);

            db.SaveChanges();

            var newRecordViewModel = new RecordListViewModel {
                DateTime = model.DateTime,
                PriorityBox = db.BoxesSet.FirstOrDefault(x => x.BoxId == model.PriorityBox.BoxId),
                PriorityCarWasher = db.CarWasherSet.FirstOrDefault(x => x.CarWasherId == model.PriorityCarWasher.CarWasherId),
                CurrentCar = db.CarSet.FirstOrDefault(x => x.Model == model.CurrentCar.Model),
                RecordGuid = model.RecordGuid
            };
            newRecordViewModel.ListOfServices = db.ServicesSet.ToList();            
            return View(newRecordViewModel);
        }

        [Authorize]
        public ActionResult ShowRecordList(RecordSet model)
        {
            //var record = db.RecordSet.FirstOrDefault(x => x.RecordId )
            var recordViewModel = new RecordListViewModel();
            recordViewModel.PriorityBox = db.BoxesSet.FirstOrDefault(x => x.BoxId == model.BoxesBoxId);
            recordViewModel.PriorityCarWasher = db.CarWasherSet.FirstOrDefault(x => x.CarWasherId == model.CarWasherCarWasherId);
            recordViewModel.DateTime = model.DateTime;
            recordViewModel.RecordGuid = model.RecordId;
            recordViewModel.CurrentCar = db.CarSet.FirstOrDefault(x => x.CarId ==
                db.CarClientSet.FirstOrDefault(m => m.CarNumber == model.CarClientCarNumber).CarCarId);

            return View(recordViewModel);
        }

        [Authorize]
        public PartialViewResult ShowRecordListPartial(Guid _recordGuid)
        {
            //доделать сумму
            var listOfViewModel = new AddServiceViewModel();
            var currentOrder = db.OrderSet.FirstOrDefault(x => x.RecordRecordId == _recordGuid);
            List<ServicesByCategorySet> listOfCategoryServices = currentOrder.ServicesByCategorySet.ToList();
            listOfViewModel.listOfCategorySet = listOfCategoryServices;

            List<Services> listOfServices = new List<Services>();
            foreach (var item in listOfCategoryServices)
            {
                listOfServices.Add(db.ServicesSet.FirstOrDefault(x => x.CodeService == item.ServicesCodeService));
            }
            listOfViewModel.listOfServices = listOfServices;
            return PartialView(listOfCategoryServices);
        }

        public string ConnectServiceAndCategory(int _codeService)
        {
            var currentService = db.ServicesSet.FirstOrDefault(x => x.CodeService == _codeService);
            return currentService.Description;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult CreateNonAuthenticatedRecord()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNonAuthenticatedRecord(RecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var date = Convert.ToDateTime(model.DateRecord.Day+"/"+model.DateRecord.Month+"/"+
                    model.DateRecord.Year+" "+ model.TimeRecord.Hour+":"+model.TimeRecord.Minute+":"+model.TimeRecord.Second, CultureInfo.InvariantCulture);                                
                            
                var newRecord = db.RecordSet.Create();
                newRecord.DateTime = date;
                newRecord.CarClientCarNumber = "A000AA000";
                newRecord.RecordId = Guid.NewGuid();

                db.RecordSet.Add(newRecord);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "MainPage");
        }
    }
}