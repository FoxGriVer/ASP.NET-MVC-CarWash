using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CarWash.Models;

namespace CarWash.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        CarWashModelContainer db = new CarWashModelContainer();

        //БЛОК ЛИЧНОГО КАБИНЕТА
        public ActionResult ManageUser()
        {
            return View();
        }

        public ActionResult CurrentCarInformationPartial()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentAppClient = UserManager.Users.FirstOrDefault(x => x.Id == userId);
            List<CarClientSet> currentCarClient = db.CarClientSet.Where(x => x.ClientClientId == currentAppClient.ClientGuid).ToList();

            List<CheckCar> listOfChechedCars = new List<Models.CheckCar>();

            foreach (var item in currentCarClient)
            {
                var checkedCar = new CheckCar();
                var carsModel = db.RecordSet.FirstOrDefault(x => x.CarClientCarNumber == item.CarNumber);

                checkedCar.currentCar = item;
                if (carsModel == null)
                {
                    checkedCar.hasRecords = false;
                }
                else
                {
                    checkedCar.hasRecords = true;
                }
                listOfChechedCars.Add(checkedCar);
            }
            
            return PartialView(listOfChechedCars);
        }

        public ActionResult DeleteCar(string _carNumber)
        {
            CarClientSet deleteCar = db.CarClientSet.FirstOrDefault(x => x.CarNumber == _carNumber);
            var carsModel = db.RecordSet.FirstOrDefault(x => x.CarClientCarNumber == deleteCar.CarNumber);
            var answer = "Невозможно удалить запись";
            if (carsModel == null)
            {
                db.CarClientSet.Remove(deleteCar);
                db.SaveChanges();
                answer = "Запись успешно удалена";
            }            
                        
            return RedirectToAction("HandleDelete", new { answer });
        }
        
        public ActionResult HandleDelete(string answer)
        {
            ViewBag.answer = answer;
            return View();
        }



        public bool CheckCar(Guid _carGuid)
        {
            bool hasRecord = false;
            CarClientSet deleteCar = db.CarClientSet.FirstOrDefault(x => x.CarCarId == _carGuid);
            var carsModel = db.RecordSet.FirstOrDefault(x => x.CarClientCarNumber == deleteCar.CarNumber);
            if (carsModel == null)
            {                
                hasRecord = true;
            }
            return hasRecord;
        }

        public string ConnectCarAndCarClient(Guid CarClientGuid)
        {
            Car car = db.CarSet.FirstOrDefault(x => x.CarId == CarClientGuid);
            return car.Mark + " " + car.Model;
        }
        

        public ActionResult PriorityInformationPartial()
        {
            var boxesList = db.BoxesSet.ToList();
            var carWashersList = db.CarWasherSet.ToList();
            var userId = User.Identity.GetUserId();
            ApplicationUser currentAppClient = UserManager.Users.FirstOrDefault(x => x.Id == userId);

            var priorityInformModel = new PriorityInformationViewModel { Boxes = boxesList,
                CarWashers = carWashersList, CurrentClient = currentAppClient.ClientGuid};

            return PartialView(priorityInformModel);
        }

        [HttpPost]
        public ActionResult PriorityInformationPartial(PriorityInformationViewModel model)
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentAppClient = UserManager.Users.FirstOrDefault(x => x.Id == userId);
            var carClient = db.CarClientSet.FirstOrDefault(x => x.ClientClientId == currentAppClient.ClientGuid);

            carClient.PriorityBox = model.PriotityBox.BoxId;
            carClient.PriorityCarWasher = model.PriorityCarWasher.CarWasherId;
            db.SaveChanges();
            
            return RedirectToAction("ManageUser");
        }

        public ActionResult CurrentPriorityInformationPartial()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentAppClient = UserManager.Users.FirstOrDefault(x => x.Id == userId);
            var carClient = db.CarClientSet.FirstOrDefault(x => x.ClientClientId == currentAppClient.ClientGuid);
            if(carClient != null)
            {
                //if (carClient.PriorityBox != null)
                //{
                    var priorityInformModel = new PriorityInformationViewModel();
                    priorityInformModel.PriotityBox = db.BoxesSet.FirstOrDefault(x => x.BoxId == carClient.PriorityBox);
                    priorityInformModel.PriorityCarWasher = db.CarWasherSet.FirstOrDefault(x => x.CarWasherId == carClient.PriorityCarWasher);
                    return PartialView(priorityInformModel);
                //}                
            }
            else
            {
                var priorityInformModel = new PriorityInformationViewModel();
                priorityInformModel.PriorityCarWasher = new CarWasher();
                priorityInformModel.PriotityBox = new Boxes();

                priorityInformModel.PriotityBox.Description = "Не указан";
                priorityInformModel.PriorityCarWasher.Name = "Не указан";
                return PartialView(priorityInformModel);
            }
        }

        public ActionResult CarInformationPartial()
        {
            var carsList = db.CarSet.ToList();
            var carsModel = new CarViewModel { Cars = carsList };
            return PartialView(carsModel);
        }

        [HttpPost]
        public ActionResult CarInformationPartial(CarViewModel model)
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentAppClient = UserManager.Users.FirstOrDefault(x => x.Id == userId);
            Client currentClient = db.ClientSet.FirstOrDefault(x => x.ClientId == currentAppClient.ClientGuid);

            var newCarClient = db.CarClientSet.Create();            
            newCarClient.CarNumber = model.ClientsCarNumber;
            newCarClient.ClientClientId = currentClient.ClientId;
            newCarClient.CarCarId = model.CurrentModel;
            db.CarClientSet.Add(newCarClient);
            db.SaveChanges();

            return RedirectToAction("ManageUser");
        }

        public ActionResult UserInformationPartial()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentAppClient = UserManager.Users.FirstOrDefault(x => x.Id == userId);
            Client currentClient = db.ClientSet.FirstOrDefault(x => x.ClientId == currentAppClient.ClientGuid);
            ViewBag.currentAppClient = currentAppClient;
            ViewBag.currentClient = currentClient;
            return PartialView();
        }

        // ...........................................

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : message == ManageMessageId.SetPasswordSuccess ? "Пароль задан."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Настроен поставщик двухфакторной проверки подлинности."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : message == ManageMessageId.AddPhoneSuccess ? "Ваш номер телефона добавлен."
                : message == ManageMessageId.RemovePhoneSuccess ? "Ваш номер телефона удален."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Создание и отправка маркера
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Ваш код безопасности: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Отправка SMS через поставщик SMS для проверки номера телефона
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // Это сообщение означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Не удалось проверить телефон");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Это сообщение означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Внешнее имя входа удалено."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}