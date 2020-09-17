using Business.Abstract;
using Desktop.Pages;
using Entities.Concrete;
using System.Windows;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_VehicleCreate.xaml
    /// </summary>
    public partial class W_VehicleCreate : Window
    {
        private IVehicleService _vehicleService;
        private IVehicleTypeService _vehicleTypeService;
        private IBrandService _brandService;
        private IModelService _modelService;
        private Employee _employee;
        public W_VehicleCreate(IVehicleService vehicleService, IVehicleTypeService vehicleTypeService, IBrandService brandService, IModelService modelService, Employee employee)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
            _modelService = modelService;
            _employee = employee;
            InitializeComponent();
        }

        public string Plate { get; set; }

        private P_VehicleCreate page;

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            page = new P_VehicleCreate(_vehicleService, _vehicleTypeService, _brandService, _modelService, _employee);
            page.textBox_Plate.Text = Plate;
            frame.Navigate(page);
        }
    }
}