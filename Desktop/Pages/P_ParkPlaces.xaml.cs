using Business.Abstract;
using Desktop.Classes;
using Desktop.UserControls;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_ParkPlaces.xaml
    /// </summary>
    public partial class P_ParkPlaces : Page
    {
        private IParkPlaceService _parkPlaceService;
        private IVehicleService _vehicleService;
        private IPersonService _personService;
        private IParkService _parkService;
        private ICustomerService _customerService;
        private IVehicleTypeService _vehicleTypeService;
        private IBrandService _brandService;
        private IModelService _modelService;
        private ICampaignService _campaignService;
        private IVehiclePriceService _vehiclePriceService;
        private IParkHistoryService _parkHistoryService;
        private Employee _employee;
        public P_ParkPlaces(IVehicleService vehicleService, IVehiclePriceService vehiclePriceService, ICampaignService campaignService, IVehicleTypeService vehicleTypeService, IBrandService brandService, IModelService modelService, IParkHistoryService parkHistoryService, IPersonService personService, IParkService parkService, ICustomerService customerService, IParkPlaceService parkPlaceService, Employee employee)
        {
            _vehicleService = vehicleService;
            _vehiclePriceService = vehiclePriceService;
            _campaignService = campaignService;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
            _modelService = modelService;
            _parkHistoryService = parkHistoryService;
            _personService = personService;
            _parkService = parkService;
            _customerService = customerService;
            _parkPlaceService = parkPlaceService;
            _employee = employee;

            InitializeComponent();

            page.Background = Settings.GetParkingLotImage();

        }

        private List<ParkPlace> ParkPlaces { get; set; }

        public void LoadParkPlaces()
        {
            ParkPlaces = _parkPlaceService.ListByNonDeleted();

            foreach (var item in ParkPlaces)
            {
                UC_ParkPlace uc_ParkPlace = new UC_ParkPlace(_vehicleService, _vehiclePriceService, _campaignService, _vehicleTypeService, _brandService, _modelService, _parkHistoryService, _personService, _parkService, _customerService, item, _employee, false)
                {
                    Width = item.Width,
                    Height = item.Height
                };
                canvas.Children.Add(uc_ParkPlace);
                Canvas.SetLeft(uc_ParkPlace, item.X);
                Canvas.SetTop(uc_ParkPlace, item.Y);
            }
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadParkPlaces();

            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}