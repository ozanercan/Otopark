using Business.Abstract;
using Business.Abstract.Apis;
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
        public W_Welcome(ILicenseService licenseService, IApiLicenseService apiLicenseService, ILocalLicenseService localLicenseService, IBrandService brandService, ICampaignService campaignService, ICustomerService customerService, IEmployeeService employeeService, IModelService modelService, IParkHistoryService parkHistoryService, IParkPlaceService parkPlaceService, IParkService parkService, IPersonService personService, IVehicleService vehicleService, IVehiclePriceService vehiclePriceService, IVehicleTypeService vehicleTypeService)
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
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                label_Version.Content = "Ver: " + CommonOperations.Application.GetVersion();
                if (_localLicenseService.GetALicense() == null) // Sqlite'de lisans satırı yoksa
                {
                    W_License w_license = new W_License(_apiLicenseService, _localLicenseService);
                    w_license.Show();
                    this.Hide();
                }
                else // Sqlitede lisans satırı bulunduysa 
                {
                    bool canUse = _licenseService.CheckLicense();
                    if (canUse == false)
                    {
                        MessageBoxResult messageBoxQuestion = MessageBox.Show("Lisans Son Kullanım Süreniz Bitmiş! Yeni Lisans Kodu Girmek İstiyor Musunuz?", "UYARI", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (messageBoxQuestion == MessageBoxResult.Yes)
                        {
                            W_License w_license = new W_License(_apiLicenseService, _localLicenseService);
                            w_license.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Program Sonlandırılıyor!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Warning);
                            CommonOperations.Application.Stop();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void grid_Support_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CommonOperations.StartLiveChat();
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            W_Login w_Login = new W_Login(_licenseService, _apiLicenseService, _localLicenseService, _brandService, _campaignService, _customerService, _employeeService, _modelService, _parkHistoryService, _parkPlaceService, _parkService, _personService, _vehicleService, _vehiclePriceService, _vehicleTypeService);
            w_Login.Show();
            this.Hide();
        }
    }
}
