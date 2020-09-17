using Business.Abstract;
using Desktop.Classes;
using Desktop.Pages;
using Entities.Concrete;
using System.Windows;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_MainMenu.xaml
    /// </summary>
    public partial class W_MainMenu : Window
    {
        private Employee _employee;

        #region Servisler
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

        public W_MainMenu(IBrandService brandService, ICampaignService campaignService, ICustomerService customerService, IEmployeeService employeeService, IModelService modelService, IParkHistoryService parkHistoryService, IParkPlaceService parkPlaceService, IParkService parkService, IPersonService personService, IVehicleService vehicleService, IVehiclePriceService vehiclePriceService, IVehicleTypeService vehicleTypeService, Employee employee)
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

            _employee = employee;
            InitializeComponent();
            window.Background = Settings.GetBackgroundImage();
        }



        #region Sayfalar
        private P_ParkList p_ParkList;

        private P_ParkPlaces p_ParkPlaces;
        private P_ParkPlaceOperations p_ParkPlaceOperations;

        private P_CustomerList p_CustomerList;
        private P_CustomerCreate p_CustomerCreate;
        private P_CustomerUpdatendDelete p_CustomerUpdatendDelete;

        private P_EmployeeList p_EmployeeList;
        private P_EmployeeCreate p_EmployeeCreate;
        private P_EmployeeUpdatendDelete p_EmployeeUpdatendDelete;

        private P_VehicleList p_VehicleList;
        private P_VehicleCreate P_VehicleCreate;
        private P_VehicleUpdatendDelete p_VehicleUpdatendDelete;

        private P_VehicleTypeList p_VehicleTypeList;
        private P_VehicleTypeCreate p_VehicleTypeCreate;
        private P_VehicleTypeUpdatendDelete p_VehicleTypeUpdatendDelete;

        private P_VehiclePriceCreate p_VehiclePriceCreate;
        private P_VehiclePriceList p_VehiclePriceList;
        private P_VehiclePriceUpdatendDelete p_VehiclePriceUpdatendDelete;

        private P_ParkHistoryList p_ParkHistoryList;

        private P_BrandList p_BrandList;
        private P_BrandCreate p_BrandCreate;
        private P_BrandUpdatendDelete p_BrandUpdatendDelete;

        private P_ModelList p_ModelList;
        private P_ModelCreate p_ModelCreate;
        private P_ModelUpdatendDelete p_ModelUpdatendDelete;
        #endregion

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            menuItem_ParkPlace_Click(sender, e);
        }

        private void menuItem_ParkPlace_Click(object sender, RoutedEventArgs e)
        {
            p_ParkPlaces = new P_ParkPlaces(_vehicleService, _vehiclePriceService, _campaignService, _vehicleTypeService, _brandService, _modelService, _parkHistoryService, _personService, _parkService, _customerService, _parkPlaceService, _employee);
            frame.Navigate(p_ParkPlaces);
        }

        private void menuItem_ParkList_Click(object sender, RoutedEventArgs e)
        {
            p_ParkList = new P_ParkList(_parkService);
            frame.Navigate(p_ParkList);
        }

        private void menuItem_CustomerCreate_Click(object sender, RoutedEventArgs e)
        {
            p_CustomerCreate = new P_CustomerCreate(_personService, _customerService);
            frame.Navigate(p_CustomerCreate);
        }

        private void menuItem_EmployeeCreate_Click(object sender, RoutedEventArgs e)
        {
            p_EmployeeCreate = new P_EmployeeCreate(_personService, _employeeService);
            frame.Navigate(p_EmployeeCreate);
        }

        private void menuItem_VehicleCreate_Click(object sender, RoutedEventArgs e)
        {
            P_VehicleCreate = new P_VehicleCreate(_vehicleService, _vehicleTypeService, _brandService, _modelService, _employee);
            frame.Navigate(P_VehicleCreate);
        }

        private void menuItem_ParkPlaceOperation_Click(object sender, RoutedEventArgs e)
        {
            p_ParkPlaceOperations = new P_ParkPlaceOperations(_parkPlaceService, _employee);
            frame.Navigate(p_ParkPlaceOperations);
        }

        private void menuItem_VehicleTypeCreate_Click(object sender, RoutedEventArgs e)
        {
            p_VehicleTypeCreate = new P_VehicleTypeCreate(_vehicleTypeService);
            frame.Navigate(p_VehicleTypeCreate);
        }

        private void menuItem_VehicleTypeUpdatendDelete_Click(object sender, RoutedEventArgs e)
        {
            p_VehicleTypeUpdatendDelete = new P_VehicleTypeUpdatendDelete(_vehicleTypeService);
            frame.Navigate(p_VehicleTypeUpdatendDelete);
        }

        private void menuItem_VehicleTypeList_Click(object sender, RoutedEventArgs e)
        {
            p_VehicleTypeList = new P_VehicleTypeList(_vehicleTypeService);
            frame.Navigate(p_VehicleTypeList);
        }

        private void menuItem_VehicleList_Click(object sender, RoutedEventArgs e)
        {
            p_VehicleList = new P_VehicleList(_vehicleService);
            frame.Navigate(p_VehicleList);
        }

        private void menuItem_VehicleUpdatendDelete_Click(object sender, RoutedEventArgs e)
        {
            p_VehicleUpdatendDelete = new P_VehicleUpdatendDelete(_vehicleService, _vehicleTypeService, _employeeService, _brandService, _modelService);
            frame.Navigate(p_VehicleUpdatendDelete);
        }

        private void menuItem_EmployeeList_Click(object sender, RoutedEventArgs e)
        {
            p_EmployeeList = new P_EmployeeList(_employeeService);
            frame.Navigate(p_EmployeeList);
        }

        private void menuItem_EmployeeUpdatendDelete_Click(object sender, RoutedEventArgs e)
        {
            p_EmployeeUpdatendDelete = new P_EmployeeUpdatendDelete(_personService, _employeeService);
            frame.Navigate(p_EmployeeUpdatendDelete);
        }

        private void menuItem_CustomerList_Click(object sender, RoutedEventArgs e)
        {
            p_CustomerList = new P_CustomerList(_customerService);
            frame.Navigate(p_CustomerList);
        }

        private void menuItem_CustomerUpdatendDelete_Click(object sender, RoutedEventArgs e)
        {
            p_CustomerUpdatendDelete = new P_CustomerUpdatendDelete(_customerService, _personService, _employee);
            frame.Navigate(p_CustomerUpdatendDelete);
        }

        private void menuItem_PriceCreate_Click(object sender, RoutedEventArgs e)
        {
            p_VehiclePriceCreate = new P_VehiclePriceCreate(_vehicleTypeService, _vehiclePriceService);
            frame.Navigate(p_VehiclePriceCreate);
        }

        private void menuItem_PriceList_Click(object sender, RoutedEventArgs e)
        {
            p_VehiclePriceList = new P_VehiclePriceList(_vehiclePriceService);
            frame.Navigate(p_VehiclePriceList);
        }

        private void menuItem_PriceUpdatendDelete_Click(object sender, RoutedEventArgs e)
        {
            p_VehiclePriceUpdatendDelete = new P_VehiclePriceUpdatendDelete(_vehiclePriceService, _vehicleTypeService);
            frame.Navigate(p_VehiclePriceUpdatendDelete);
        }

        private void menuItem_ParkHistoryList_Click(object sender, RoutedEventArgs e)
        {
            p_ParkHistoryList = new P_ParkHistoryList(_parkHistoryService);
            frame.Navigate(p_ParkHistoryList);
        }

        private void menuItem_BrandList_Click(object sender, RoutedEventArgs e)
        {
            p_BrandList = new P_BrandList(_brandService);
            frame.Navigate(p_BrandList);
        }

        private void menuItem_BrandCreate_Click(object sender, RoutedEventArgs e)
        {
            p_BrandCreate = new P_BrandCreate(_brandService);
            frame.Navigate(p_BrandCreate);
        }

        private void menuItem_BrandUpdatendDelete_Click(object sender, RoutedEventArgs e)
        {
            p_BrandUpdatendDelete = new P_BrandUpdatendDelete(_brandService);
            frame.Navigate(p_BrandUpdatendDelete);
        }

        private void menuItem_ModelList_Click(object sender, RoutedEventArgs e)
        {
            p_ModelList = new P_ModelList(_modelService);
            frame.Navigate(p_ModelList);
        }

        private void menuItem_ModelCreate_Click(object sender, RoutedEventArgs e)
        {
            p_ModelCreate = new P_ModelCreate(_brandService, _modelService);
            frame.Navigate(p_ModelCreate);
        }

        private void menuItem_ModelUpdatendDelete_Click(object sender, RoutedEventArgs e)
        {
            p_ModelUpdatendDelete = new P_ModelUpdatendDelete(_brandService, _modelService);
            frame.Navigate(p_ModelUpdatendDelete);
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CommonOperations.Application.Stop();
        }
    }
}