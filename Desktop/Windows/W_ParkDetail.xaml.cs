using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.ComponentModel;
using System.Windows;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_ParkDetail.xaml
    /// </summary>
    public partial class W_ParkDetail : Window, INotifyPropertyChanged
    {
        private IParkService _parkService;
        private IPersonService _personService;
        private ICustomerService _customerService;
        private IVehicleService _vehicleService;
        private ICampaignService _campaignService;
        private IParkHistoryService _parkHistoryService;
        private IVehiclePriceService _vehiclePriceService;
        public W_ParkDetail(IParkService parkService, IPersonService personService, ICustomerService customerService, IVehicleService vehicleService, ICampaignService campaignService, IParkHistoryService parkHistoryService, IVehiclePriceService vehiclePriceService, ParkPlace parkPlace, Employee employee)
        {
            _parkService = parkService;
            _personService = personService;
            _customerService = customerService;
            _vehicleService = vehicleService;
            _campaignService = campaignService;
            _parkHistoryService = parkHistoryService;
            _vehiclePriceService = vehiclePriceService;

            InitializeComponent();
            Employee = employee;
            OnPropertyChanged("Employee");
            ParkPlace = parkPlace;
            OnPropertyChanged("ParkPlace");
        }



        public Park Park { get; set; }
        public ParkPlace ParkPlace { get; set; }
        public Employee Employee { get; set; }

        public Customer Customer { get; set; }
        public Person Person { get; set; }

        public Vehicle Vehicle { get; set; }
        public Campaign Campaign { get; set; }
        public VehiclePrice VehiclePrice { get; set; }

        private void List()
        {
            if (Park != null) Park = null;

            Park = _parkService.GetByParkPlaceIdandNonDeleted(ParkPlace.Id);
            OnPropertyChanged("Park");
        }

        private void GetPersonNationalityNumber()
        {
            if (Park != null && Park.CustomerId.HasValue)
            {
                Customer = _customerService.Find(Park.CustomerId.Value);
                OnPropertyChanged("Customer");
                if (Customer != null)
                {
                    Person = _personService.Find(Customer.PersonId);
                    OnPropertyChanged("Person");
                }
            }
        }

        private void GetVehiclePlate()
        {
            if (Park != null)
            {
                Vehicle = _vehicleService.Find((int)Park.VehicleId);
                OnPropertyChanged("Vehicle");
            }
        }

        private void GetCampaign()
        {
            if (Park != null && Park.CampaignId.HasValue)
            {
                Campaign = _campaignService.Find((int)Park.CampaignId);
            }
        }

        private void PriceCalculate()
        {
            try
            {
                if (Park != null)
                {
                    VehiclePrice = _vehiclePriceService.Calculate(Vehicle.VehicleTypeId, Park.ParkingStartDateTime);
                    if (VehiclePrice == null)
                    {
                        if (MessageBox.Show(string.Format(Languages.LocalizationFile.PriceMethodQuestionText), string.Format(Languages.LocalizationFile.WarningText), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            PriceTextBoxShow();
                        else
                            VehiclePrice = _vehiclePriceService.GetMaxPrice(Vehicle.VehicleTypeId);
                    }
                    OnPropertyChanged("VehiclePrice");
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void PriceTextBoxShow()
        {
            label_Price.Visibility = Visibility.Hidden;
            textBox_Price.Visibility = Visibility.Visible;
        }

        private void PriceLabelShow()
        {
            label_Price.Visibility = Visibility.Visible;
            textBox_Price.Visibility = Visibility.Hidden;
        }

        private void button_ParkFinished_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double price;
                if (VehiclePrice == null && (textBox_Price.Text != String.Empty && textBox_Price.Text.Length > 0))
                    price = Convert.ToDouble(textBox_Price.Text);
                else
                    price = VehiclePrice.Price;

                ParkHistory parkHistory = new ParkHistory()
                {
                    ParkPlaceId = Park.ParkPlaceId,
                    CampaignId = Park.CampaignId,
                    CustomerId = Park.CustomerId,
                    VehicleId = Park.VehicleId,
                    EmployeeId = Park.EmployeeId,
                    Price = price,
                    ParkingStartDateTime = Park.ParkingStartDateTime,
                    ParkedLeaveDateTime = DateTime.Now,
                    CreationDateTime = Park.CreationDateTime,
                    IsDeleted = Park.IsDeleted
                };
                _parkHistoryService.Create(parkHistory);

                _parkService.Delete(Park.Id);

                MessageBox.Show(Languages.LocalizationFile.ParkDetailFinished, Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, Languages.LocalizationFile.ErrorText, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List();
                GetPersonNationalityNumber();
                GetVehiclePlate();
                GetCampaign();
                PriceCalculate();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void button_VehicleDetail_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button_CustomerDetail_Click(object sender, RoutedEventArgs e)
        {
        }

        private void menuItem_ManuelPrice_Click(object sender, RoutedEventArgs e)
        {
            PriceTextBoxShow();
        }

        private void menuItem_AutomaticPrice_Click(object sender, RoutedEventArgs e)
        {
            PriceCalculate();
            PriceLabelShow();
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