using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_ParkDetailCreate.xaml
    /// </summary>
    public partial class W_ParkDetailCreate : Window, INotifyPropertyChanged
    {
        private IVehicleService _vehicleService;
        private IPersonService _personService;
        private IParkService _parkService;
        private ICustomerService _customerService;
        private IVehicleTypeService _vehicleTypeService;
        private IBrandService _brandService;
        private IModelService _modelService;
        private Employee _employee;
        public W_ParkDetailCreate(IVehicleService vehicleService, IVehicleTypeService vehicleTypeService, IBrandService brandService, IModelService modelService, IPersonService personService, IParkService parkService, ICustomerService customerService, ParkPlace parkPlace, Employee employee)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
            _modelService = modelService;
            _personService = personService;
            _parkService = parkService;
            _customerService = customerService;
            _employee = employee;
            ParkPlace = parkPlace;

            InitializeComponent();
        }



        public ParkPlace ParkPlace { get; set; }
        private List<Person> Persons { get; set; }
        private List<Vehicle> Vehicles { get; set; }

        private void ListVehiclePlates()
        {
            if (Vehicles != null) Vehicles.Clear();
            comboBox_Plate.Items.Clear();

            Vehicles = _vehicleService.List();
            foreach (var item in Vehicles)
                comboBox_Plate.Items.Add(item.Plate);
        }

        private void ListPersonNationalityIdentityNumbers()
        {
            if (Persons != null) Persons.Clear();
            comboBox_PersonNationalityIdentityNumber.Items.Clear();

            Persons = _personService.List();
            foreach (var item in Persons)
                comboBox_PersonNationalityIdentityNumber.Items.Add(item.NationalIdentityNumber);
        }

        private Vehicle FindVehicleByPlate(string VehiclePlate)
        {
            Vehicle vehicle = null;
            foreach (var item in Vehicles)
            {
                if (item.Plate == VehiclePlate)
                    vehicle = item;
            }
            return vehicle;
        }

        private Person FindPersonByNationalIdentityNumber(string NationalIdentityNumber)
        {
            Person p = null;
            foreach (var item in Persons)
            {
                if (item.NationalIdentityNumber == NationalIdentityNumber)
                    p = item;
            }
            return p;
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListVehiclePlates();
                ListPersonNationalityIdentityNumbers();

                datePicker_Date.Text = DateTime.Now.ToString();
                textBox_Clock.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_ParkCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer findedCustomer = null;
                if (comboBox_PersonNationalityIdentityNumber.Text.Length > 2)
                {
                    Person findedPerson = this.FindPersonByNationalIdentityNumber(comboBox_PersonNationalityIdentityNumber.Text);
                    findedCustomer = _customerService.GetCustomerByPersonId(findedPerson.Id);
                }

                Vehicle findVehicle = FindVehicleByPlate(comboBox_Plate.Text);

                string[] time = textBox_Clock.Text.Split(':');
                int hour = Convert.ToInt32(time[0]);
                int minute = Convert.ToInt32(time[1]);

                int year = datePicker_Date.SelectedDate.Value.Year;
                int month = datePicker_Date.SelectedDate.Value.Month;
                int day = datePicker_Date.SelectedDate.Value.Day;

                DateTime parkingStartTime = new DateTime(year, month, day, hour, minute, 0);
                Park park = new Park()
                {
                    ParkPlaceId = ParkPlace.Id,
                    CampaignId = null,
                    CustomerId = (findedCustomer != null) ? findedCustomer.Id : null as int?,
                    VehicleId = findVehicle.Id,
                    EmployeeId = _employee.Id,
                    ParkingStartDateTime = parkingStartTime,
                    CreationDateTime = DateTime.Now,
                    IsDeleted = false,
                };
                _parkService.Create(park);
                MessageBox.Show(string.Format(Languages.LocalizationFile.ParkDetailCreateText), string.Format(Languages.LocalizationFile.SuccessText), MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_VehicleCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                W_VehicleCreate w_VehicleCreate = new W_VehicleCreate(_vehicleService, _vehicleTypeService, _brandService, _modelService, _employee)
                {
                    Plate = comboBox_Plate.Text
                };
                w_VehicleCreate.ShowDialog();

                ListVehiclePlates();
                comboBox_Plate.Text = w_VehicleCreate.Plate;
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_CustomerCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                W_CustomerCreate w_CustomerCreate = new W_CustomerCreate(_personService, _customerService, _employee)
                {
                    PersonNationalityNumber = comboBox_PersonNationalityIdentityNumber.Text
                };
                w_CustomerCreate.ShowDialog();

                ListPersonNationalityIdentityNumbers();
                comboBox_PersonNationalityIdentityNumber.Text = w_CustomerCreate.PersonNationalityNumber;
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        #region INotifyPropertyChanged Implementing

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Implementing
    }
}