using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ParkManager : IParkService
    {
        private IParkDal _parkDal;
        private IParkPlaceService _parkPlaceService;
        private ICampaignService _campaignService;
        private ICustomerService _customerService;
        private IPersonService _personService;
        private IVehicleService _vehicleService;
        private IEmployeeService _employeeService;

        public ParkManager(IParkDal parkDal, IParkPlaceService parkPlaceService, ICampaignService campaignService, ICustomerService customerService, IPersonService personService, IVehicleService vehicleService, IEmployeeService employeeService)
        {
            _parkDal = parkDal;
            _parkPlaceService = parkPlaceService;
            _campaignService = campaignService;
            _customerService = customerService;
            _personService = personService;
            _vehicleService = vehicleService;
            _employeeService = employeeService;
        }

        public Park GetByParkPlaceIdandNonDeleted(int ParkPlaceId)
        {
            return _parkDal.GetByParkPlaceIdandNonDeleted(ParkPlaceId);
        }

        public Park Control(int ParkPlaceId, int VehicleId, bool State)
        {
            return _parkDal.Control(ParkPlaceId, VehicleId, State);
        }

        public List<Park> ListByNonDeleted()
        {
            return _parkDal.ListByNonDeleted();
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _parkDal.ChangeIsDeleteField(Id, isDeleted);
        }
        public List<ParkDto> ListDto()
        {
            var parks = this.List();
            var parkPlaces = _parkPlaceService.List();
            var campaigns = _campaignService.List();
            var persons = _personService.List();
            var vehicles = _vehicleService.List();
            var employees = _employeeService.List();
            var customers = _customerService.List();

            var parkDtos = new List<ParkDto>();
            foreach (var park in parks)
            {
                parkDtos.Add(new ParkDto()
                {
                    Id = park.Id,
                    ParkPlaceName = FindParkPlaceName(park, parkPlaces),
                    CampaignName = FindCampaignName(campaigns, park),
                    Customer = FindCustomerFullName(customers, persons, park),
                    Plate = FindVehiclePlate(vehicles, park),
                    Employee = FindEmployeeFullName(persons, employees, park),
                    ParkingStartDateTime = park.ParkingStartDateTime,
                    CreationDateTime = park.CreationDateTime,
                    IsState = park.IsDeleted
                });
            }
            return parkDtos;
        }


        private string FindEmployeeFullName(List<Person> persons, List<Employee> employees, Park park)
        {
            string employeeFullName = "";
            foreach (var employee in employees)
            {
                if (park.EmployeeId == employee.Id)
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
        private string FindVehiclePlate(List<Vehicle> vehicles, Park park)
        {
            string plate = "";
            foreach (var vehicle in vehicles)
                if (park.VehicleId == vehicle.Id)
                    plate = vehicle.Plate;

            if (string.IsNullOrEmpty(plate)) plate = "-Bulunamadı-";

            return plate;
        }
        private string FindCustomerFullName(List<Customer> customers, List<Person> persons, Park park)
        {
            string customerFullName = "";
            foreach (var customer in customers)
            {
                if (park.CustomerId == customer.Id)
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
        private string FindParkPlaceName(Park park, List<ParkPlace> parkPlaces)
        {
            string parkPlaceName = "";
            foreach (var parkPlace in parkPlaces)
                if (park.ParkPlaceId == parkPlace.Id)
                    parkPlaceName = parkPlace.Name;

            if (string.IsNullOrEmpty(parkPlaceName)) parkPlaceName = "-Bulunamadı-";

            return parkPlaceName;
        }
        private string FindCampaignName(List<Campaign> campaigns, Park park)
        {
            string campaignName = "";
            foreach (var campaign in campaigns)
                if (park.CampaignId == campaign.Id)
                    campaignName = campaign.Name;

            if (string.IsNullOrEmpty(campaignName)) campaignName = "-Bulunamadı-";

            return campaignName;
        }

        public List<Park> List()
        {
            return _parkDal.List();
        }

        public int Create(Park park)
        {
            return _parkDal.Create(park);
        }

        public void Update(Park park)
        {
            _parkDal.Update(park);
        }

        public void Delete(int Id)
        {
            _parkDal.Delete(Id);
        }

        public List<ParkDto> ListDtoByNonDeleted()
        {
            var parks = this.ListByNonDeleted();
            var parkPlaces = _parkPlaceService.List();
            var campaigns = _campaignService.List();
            var persons = _personService.List();
            var vehicles = _vehicleService.List();
            var employees = _employeeService.List();
            var customers = _customerService.List();

            var parkDtos = new List<ParkDto>();
            foreach (var park in parks)
            {
                parkDtos.Add(new ParkDto()
                {
                    Id = park.Id,
                    ParkPlaceName = FindParkPlaceName(park, parkPlaces),
                    CampaignName = FindCampaignName(campaigns, park),
                    Customer = FindCustomerFullName(customers, persons, park),
                    Plate = FindVehiclePlate(vehicles, park),
                    Employee = FindEmployeeFullName(persons, employees, park),
                    ParkingStartDateTime = park.ParkingStartDateTime,
                    CreationDateTime = park.CreationDateTime,
                    IsState = park.IsDeleted
                });
            }
            return parkDtos;
        }
    }
}