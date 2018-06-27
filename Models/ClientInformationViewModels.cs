using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarWash.Models;
using System.ComponentModel.DataAnnotations;


namespace CarWash.Models
{   
    public class PriorityInformationViewModel
    {
        public List<Boxes> Boxes { get; set; }
        public List<CarWasher> CarWashers { get; set; }
        public CarWasher PriorityCarWasher { get; set; }
        public Boxes PriotityBox { get; set; }
        public Guid CurrentClient { get; set; }
    }

    public class RecordViewModel
    {
        public List<Boxes> Boxes { get; set; }
        public List<CarWasher> CarWashers { get; set; }
        public string PriorityCarWasher { get; set; }
        public int PriotityBox { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата записи")]
        public DateTime DateRecord { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Время записи")]
        public DateTime TimeRecord { get; set; }
    }

    public class AutorizedUserRecord
    {
        public List<Car> UserCars { get; set; }
        public Car CurrentCar { get; set; }
        public Guid ClientGuid { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата записи")]
        public DateTime DateRecord { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Время записи")]
        public DateTime TimeRecord { get; set; }
    }

    public class CarViewModel
    {
        public List<Car> Cars { get; set; }
        public Guid CurrentModel { get; set; }
        public string ClientsCarNumber { get; set; }
        public string CurrentMark { get; set; }
        public bool HasRecord { get; set; }
    }

    public class RecordListViewModel
    {
        public Guid RecordGuid { get; set; }
        public List<Services> ListOfServices { get; set; }
        public Car CurrentCar { get; set; }
        public Services PickedService { get; set; }
        public Boxes PriorityBox { get; set; }
        public CarWasher PriorityCarWasher { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class AddServiceViewModel
    {
        public Guid RecordGuid { get; set; }
        public List<Services> listOfServices { get; set; }
        public List<ServicesByCategorySet> listOfCategorySet { get; set; }
    }

    public class CheckCar
    {
        public CarClientSet currentCar { get; set; }
        public bool hasRecords { get; set; }
    }
}