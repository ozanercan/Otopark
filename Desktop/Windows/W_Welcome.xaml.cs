using Business.Abstract;
using Desktop.Classes;
using System;
using System.Windows;
using System.Windows.Input;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_Welcome.xaml
    /// </summary>
    public partial class W_Welcome : Window
    {

        #region fieldler
        private IBrandService _brandService;
        private ICampaignService _campaignService;
        private ICustomerService _customerService;
        private IEmployeeService _employeeService;
        private IModelService _modelService;
        private IParkHistoryService _parkHistoryService;
        private IParkPlaceService _parkPlaceService;
        private IParkService _parkService;
        private IPersonService _personService;
        private IVehicleService _vehicleService;
        private IVehiclePriceService _vehiclePriceService;
        private IVehicleTypeService _vehicleTypeService;
        #endregion
        public W_Welcome(IBrandService brandService, ICampaignService campaignService, ICustomerService customerService, IEmployeeService employeeService, IModelService modelService, IParkHistoryService parkHistoryService, IParkPlaceService parkPlaceService, IParkService parkService, IPersonService personService, IVehicleService vehicleService, IVehiclePriceService vehiclePriceService, IVehicleTypeService vehicleTypeService)
        {
            _brandService = brandService;
            _campaignService = campaignService;
            _customerService = customerService;
            _employeeService = employeeService;
            _modelService = modelService;
            _parkHistoryService = parkHistoryService;
            _parkPlaceService = parkPlaceService;
            _parkService = parkService;
            _personService = personService;
            _vehicleService = vehicleService;
            _vehiclePriceService = vehiclePriceService;
            _vehicleTypeService = vehicleTypeService;

            _employeeService = employeeService;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label_Version.Content = "Ver: " + CommonOperations.Application.GetVersion();
        }

        private void grid_Support_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CommonOperations.StartLiveChat();
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            W_Login w_Login = new W_Login(_brandService, _campaignService, _customerService, _employeeService, _modelService, _parkHistoryService, _parkPlaceService, _parkService, _personService, _vehicleService, _vehiclePriceService, _vehicleTypeService);
            w_Login.Show();
            this.Hide();
        }
    }
}
