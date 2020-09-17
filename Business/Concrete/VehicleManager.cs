using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class VehicleManager : IVehicleService
    {
        private IVehicleDal _vehicleDal;
        private IVehicleTypeService _vehicleTypeService;
        private IBrandService _brandService;
        private IModelService _modelService;
        private IPersonService _personService;
        private IEmployeeService _employeeService;

        public VehicleManager(IVehicleDal vehicleDal, IVehicleTypeService vehicleTypeService, IBrandService brandService, IModelService modelService, IPersonService personService, IEmployeeService employeeService)
        {
            _vehicleDal = vehicleDal;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
            _modelService = modelService;
            _personService = personService;
            _employeeService = employeeService;
        }

        public bool ControlByPlate(string Plate)
        {
            return _vehicleDal.ControlByPlate(Plate);
        }

        public List<Vehicle> ListByNonDeleted()
        {
            return _vehicleDal.ListByNonDeleted();
        }

        public List<VehicleDto> ListDto()
        {
            var vehicles = this.List();

            var vehicleTypes = _vehicleTypeService.List();
            var employees = _employeeService.List();
            var brands = _brandService.List();
            var models = _modelService.List();
            var persons = _personService.List();

            var vehicleDtos = new List<VehicleDto>();


            foreach (var vehicle in vehicles)
            {
                vehicleDtos.Add(new VehicleDto()
                {
                    Id = vehicle.Id,
                    Employee = FindEmployeeFullName(persons, employees, vehicle),
                    Plate = vehicles.Where(i => i.Id == vehicle.Id).FirstOrDefault().Plate,
                    VehicleType = vehicleTypes.Where(i => i.Id == vehicle.VehicleTypeId).FirstOrDefault().Name,
                    Brand = brands.Where(i => i.Id == vehicle.BrandId).FirstOrDefault().Name,
                    Color = vehicle.Color,
                    Model = models.Where(i => i.Id == vehicle.ModelId).FirstOrDefault().Name,
                    IsDeleted = vehicle.IsDeleted
                });
            }

            return vehicleDtos;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _vehicleDal.ChangeIsDeleteField(Id, isDeleted);
        }
        private string FindEmployeeFullName(List<Person> persons, List<Employee> employees, Vehicle vehicle)
        {
            string employeeFullName = "";
            foreach (var employee in employees)
            {
                if (vehicle.EmployeeId == employee.Id)
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

        public List<Vehicle> List()
        {
            return _vehicleDal.List();
        }

        public int Create(Vehicle vehicle)
        {
            return _vehicleDal.Create(vehicle);
        }

        public void Update(Vehicle vehicle)
        {
            _vehicleDal.Update(vehicle);
        }

        public void Delete(int Id)
        {
            _vehicleDal.Delete(Id);
        }

        public Vehicle Find(int Id)
        {
            return _vehicleDal.Find(Id);
        }

        public List<VehicleDto> ListDtoByNonDeleted()
        {
            var vehicles = this.ListByNonDeleted();

            var vehicleTypes = _vehicleTypeService.List();
            var employees = _employeeService.List();
            var brands = _brandService.List();
            var models = _modelService.List();
            var persons = _personService.List();

            var vehicleDtos = new List<VehicleDto>();


            foreach (var vehicle in vehicles)
            {
                vehicleDtos.Add(new VehicleDto()
                {
                    Id = vehicle.Id,
                    Employee = FindEmployeeFullName(persons, employees, vehicle),
                    Plate = vehicles.Where(i => i.Id == vehicle.Id).FirstOrDefault().Plate,
                    VehicleType = vehicleTypes.Where(i => i.Id == vehicle.VehicleTypeId).FirstOrDefault().Name,
                    Brand = brands.Where(i => i.Id == vehicle.BrandId).FirstOrDefault().Name,
                    Color = vehicle.Color,
                    Model = models.Where(i => i.Id == vehicle.ModelId).FirstOrDefault().Name,
                    IsDeleted = vehicle.IsDeleted
                });
            }

            return vehicleDtos;
        }

        public void ChangeIsDeletedByVehicleId(int Id)
        {
            _vehicleDal.ChangeIsDeleteField(Id, true);
        }

        public void ChangeIsNonDeletedByVehicleId(int Id)
        {
            _vehicleDal.ChangeIsDeleteField(Id, false);
        }
    }
}