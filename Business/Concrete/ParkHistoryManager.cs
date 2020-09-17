using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ParkHistoryManager : IParkHistoryService
    {
        private IParkHistoryDal _parkHistoryDal;
        private IParkPlaceService _parkPlaceService;
        private ICampaignService _campaignService;
        private ICustomerService _customerService;
        private IPersonService _personService;
        private IVehicleService _vehicleService;
        private IEmployeeService _employeeService;

        public ParkHistoryManager(IParkHistoryDal parkHistoryDal, IParkPlaceService parkPlaceService, ICampaignService campaignService, ICustomerService customerService, IPersonService personService, IVehicleService vehicleService, IEmployeeService employeeService)
        {
            _parkHistoryDal = parkHistoryDal;
            _parkPlaceService = parkPlaceService;
            _campaignService = campaignService;
            _customerService = customerService;
            _personService = personService;
            _vehicleService = vehicleService;
            _employeeService = employeeService;
        }

        public List<ParkHistoryDto> ListDto()
        {
            // Campaign devre dışıdır.
            var parkHistories = this.List();
            var parkPlaces = _parkPlaceService.List();
            var customers = _customerService.List();
            var campaigns = _campaignService.List();
            var persons = _personService.List();
            var vehicles = _vehicleService.List();
            var employees = _employeeService.List();
            var parkHistoryDtos = new List<ParkHistoryDto>();

            foreach (var parkHistory in parkHistories)
            {
                parkHistoryDtos.Add(new ParkHistoryDto()
                {
                    Id = parkHistory.Id,
                    ParkPlaceName = FindParkPlaceName(parkPlaces, parkHistory),
                    CampaignName = FindCampaignName(campaigns, parkHistory),
                    Customer = FindCustomerFullName(customers, persons, parkHistory),
                    Plate = FindVehiclePlate(vehicles, parkHistory),
                    Employee = FindEmployeeFullName(persons, employees, parkHistory),
                    Price = parkHistory.Price,
                    ParkingStartDateTime = parkHistory.ParkingStartDateTime,
                    ParkedLeaveDateTime = parkHistory.ParkedLeaveDateTime,
                    CreationDateTime = parkHistory.CreationDateTime,
                    IsDeleted = parkHistory.IsDeleted
                });
            }

            return parkHistoryDtos;
        }

        private string FindEmployeeFullName(List<Person> persons, List<Employee> employees, ParkHistory parkHistory)
        {
            string employeeFullName = "";
            foreach (var employee in employees)
            {
                if (parkHistory.EmployeeId == employee.Id)
                {
                    foreach (var person in persons)
                    {
                        if (employee.PersonId == person.Id)
                            employeeFullName = person.FirstName + " " + person.LastName;
                    }
                }
            }

            if (string.IsNullOrEmpty(employeeFullName)) employeeFullName = "-Bulunamadı-";

            return employeeFullName;
        }

        private string FindVehiclePlate(List<Vehicle> vehicles, ParkHistory parkHistory)
        {
            string plate = "";
            foreach (var vehicle in vehicles)
                if (parkHistory.VehicleId == vehicle.Id)
                    plate = vehicle.Plate;

            if (string.IsNullOrEmpty(plate)) plate = "-Bulunamadı-";

            return plate;
        }

        private string FindCustomerFullName(List<Customer> customers, List<Person> persons, ParkHistory parkHistory)
        {
            string customerFullName = "";
            foreach (var customer in customers)
            {
                if (parkHistory.CustomerId == customer.Id)
                {
                    foreach (var person in persons)
                    {
                        if (customer.PersonId == person.Id)
                            customerFullName = person.FirstName + " " + person.LastName;
                    }
                }
            }

            if (string.IsNullOrEmpty(customerFullName)) customerFullName = "-Bulunamadı-";

            return customerFullName;
        }

        private string FindCampaignName(List<Campaign> campaigns, ParkHistory parkHistory)
        {
            string campaignName = "";
            foreach (var campaign in campaigns)
                if (parkHistory.CampaignId == campaign.Id)
                    campaignName = campaign.Name;

            if (string.IsNullOrEmpty(campaignName)) campaignName = "-Bulunamadı-";

            return campaignName;
        }

        private string FindParkPlaceName(List<ParkPlace> parkPlaces, ParkHistory parkHistory)
        {
            string parkPlaceName = "";
            foreach (var parkPlace in parkPlaces)
                if (parkHistory.ParkPlaceId == parkPlace.Id)
                    parkPlaceName = parkPlace.Name;

            if (string.IsNullOrEmpty(parkPlaceName)) parkPlaceName = "-Bulunamadı-";

            return parkPlaceName;
        }

        public int Create(ParkHistory parkHistory)
        {
            return _parkHistoryDal.Create(parkHistory);
        }

        public List<ParkHistory> List()
        {
            return _parkHistoryDal.List();
        }

        public List<ParkHistoryDto> ListDtoByNonDeleted()
        {
            // Campaign devre dışıdır.
            var parkHistories = this.ListByNonDeleted();
            var parkPlaces = _parkPlaceService.List();
            var customers = _customerService.List();
            var campaigns = _campaignService.List();
            var persons = _personService.List();
            var vehicles = _vehicleService.List();
            var employees = _employeeService.List();
            var parkHistoryDtos = new List<ParkHistoryDto>();

            foreach (var parkHistory in parkHistories)
            {
                parkHistoryDtos.Add(new ParkHistoryDto()
                {
                    Id = parkHistory.Id,
                    ParkPlaceName = FindParkPlaceName(parkPlaces, parkHistory),
                    CampaignName = FindCampaignName(campaigns, parkHistory),
                    Customer = FindCustomerFullName(customers, persons, parkHistory),
                    Plate = FindVehiclePlate(vehicles, parkHistory),
                    Employee = FindEmployeeFullName(persons, employees, parkHistory),
                    Price = parkHistory.Price,
                    ParkingStartDateTime = parkHistory.ParkingStartDateTime,
                    ParkedLeaveDateTime = parkHistory.ParkedLeaveDateTime,
                    CreationDateTime = parkHistory.CreationDateTime,
                    IsDeleted = parkHistory.IsDeleted
                });
            }

            return parkHistoryDtos;
        }

        public List<ParkHistory> ListByNonDeleted()
        {
            return _parkHistoryDal.ListByNonDeleted();
        }
    }
}