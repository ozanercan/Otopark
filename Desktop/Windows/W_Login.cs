using Business.Abstract;
using Business.Abstract.Apis;
using Desktop.Classes;
using Entities.Concrete;
using Entities.Concrete.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_Login.xaml
    /// </summary>
    public partial class W_Login : Window
    {
        #region fieldler
        private ILicenseService _licenseService;
        private IApiLicenseService _apiLicenseService;
        private ILocalLicenseService _localLicenseService;
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
        public W_Login(ILicenseService licenseService, IApiLicenseService apiLicenseService, ILocalLicenseService localLicenseService, IBrandService brandService, ICampaignService campaignService, ICustomerService customerService, IEmployeeService employeeService, IModelService modelService, IParkHistoryService parkHistoryService, IParkPlaceService parkPlaceService, IParkService parkService, IPersonService personService, IVehicleService vehicleService, IVehiclePriceService vehiclePriceService, IVehicleTypeService vehicleTypeService)
        {
            _licenseService = licenseService;
            _localLicenseService = localLicenseService;
            _apiLicenseService = apiLicenseService;
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
            _licenseService = licenseService;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");

            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Login login = new Login()
                {
                    UserName = textBox_UserName.Text,
                    Password = passwordBox_Password.Password
                };
                bool result = _employeeService.LoginControl(login);
                Employee employee = _employeeService.FindEmployeeByUsernamendPassword(login);
                if (result == true)
                {
                    _employeeService.UpdateLastLoginDateTime(employee);
                    // Hangi personelin hangi işlemleri yaptığını kayıt etmek amaçlı employee bilgisi gönderiliyor.
                    W_MainMenu w_MainMenu = new W_MainMenu(_brandService, _campaignService, _customerService, _employeeService, _modelService, _parkHistoryService, _parkPlaceService, _parkService, _personService, _vehicleService, _vehiclePriceService, _vehicleTypeService, employee);
                    w_MainMenu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Languages.LocalizationFile.UsernameorPasswordIncorrectText, Languages.LocalizationFile.WarningText, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            IOOperations.ImageFolderControl();
        }
    }
}