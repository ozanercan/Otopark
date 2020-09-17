using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_VehicleCreate.xaml
    /// </summary>
    public partial class P_VehicleCreate : Page
    {
        private IVehicleService _vehicleService;
        private IVehicleTypeService _vehicleTypeService;
        private IBrandService _brandService;
        private IModelService _modelService;
        private Employee _employee;
        public P_VehicleCreate(IVehicleService vehicleService, IVehicleTypeService vehicleTypeService, IBrandService brandService, IModelService modelService, Employee employee)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
            _modelService = modelService;
            _employee = employee;
            InitializeComponent();
        }



        private List<VehicleType> VehicleTypes { get; set; }
        private List<Brand> Brands { get; set; }
        private List<Model> Models { get; set; }

        /// <summary>
        /// Araç Tiplerini ComboBox'a ekler.
        /// </summary>
        private void ListVehicleTypes()
        {
            if (VehicleTypes != null)
                VehicleTypes.Clear();

            VehicleTypes = _vehicleTypeService.ListByNonDeleted();
            foreach (var item in VehicleTypes)
            {
                comboBox_VehicleType.Items.Add(item.Name);
            }
        }

        /// <summary>
        /// Araç Markalarını ComboBox'a ekler.
        /// </summary>
        private void ListBrands()
        {
            if (Brands != null)
                Brands.Clear();

            Brands = _brandService.List();
            foreach (var item in Brands)
                comboBox_Brand.Items.Add(item.Name);

        }

        private void ListModelsByBrandId(int BrandId)
        {
            if (Models != null)
                Models.Clear();

            Models = _modelService.ListByBrandId(BrandId);
            comboBox_Model.Items.Clear();
            foreach (var item in Models)
                comboBox_Model.Items.Add(item.Name);
        }
        /// <summary>
        /// Marka Idsini Makes propertysinde arayarak bulur.
        /// </summary>
        /// <param name="MakeName">Marka Adı</param>
        /// <returns>int</returns>
        private int FindBrandIdByMakeName(string BrandName)
        {
            int makeid = 0;
            foreach (var item in Brands)
            {
                if (item.Name == BrandName)
                {
                    makeid = item.Id;
                }
            }
            return makeid;
        }

        /// <summary>
        /// Model Idsini Models propertysinde arayarak bulur.
        /// </summary>
        /// <param name="ModelName">Model Adı</param>
        /// <returns>int</returns>
        private int FindModelIdByModelName(string ModelName)
        {
            int modelid = 0;
            foreach (var item in Models)
            {
                if (item.Name == ModelName)
                {
                    modelid = item.Id;
                }
            }
            return modelid;
        }

        /// <summary>
        /// Araç Tip Idyi VehicleTypes propertysinde arayarak bulur.
        /// </summary>
        /// <param name="VehicleTypeName">Araç Tip Adı</param>
        /// <returns>int</returns>
        private int FindVehicleTypeIdByVehicleName(string VehicleTypeName)
        {
            int vehicleid = 0;
            foreach (var item in VehicleTypes)
            {
                if (item.Name == VehicleTypeName)
                {
                    vehicleid = item.Id;
                }
            }
            return vehicleid;
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int vehicleTypeId = FindVehicleTypeIdByVehicleName(comboBox_VehicleType.Text);
                Vehicle vehicle = new Vehicle()
                {
                    VehicleTypeId = vehicleTypeId,
                    EmployeeId = _employee.Id,
                    BrandId = FindBrandIdByMakeName(comboBox_Brand.SelectedItem.ToString()),
                    ModelId = FindModelIdByModelName(comboBox_Model.SelectedItem.ToString()),
                    Plate = textBox_Plate.Text,
                    Color = textBox_Color.Text,
                    IsDeleted = false
                };
                _vehicleService.Create(vehicle);
                MessageBox.Show(string.Format(Languages.LocalizationFile.VehicleCreatedText, vehicle.Plate));
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void textBox_Plate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_vehicleService.ControlByPlate(textBox_Plate.Text))
            {
                button_Create.IsEnabled = false;
                MessageBox.Show(string.Format(Languages.LocalizationFile.VehiclePlateAlreadyRegisteredText), Languages.LocalizationFile.WarningText, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                button_Create.IsEnabled = true;
            }
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListVehicleTypes();
                ListBrands();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void comboBox_Brand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int findedMakeId = FindBrandIdByMakeName(comboBox_Brand.SelectedItem.ToString());
            ListModelsByBrandId(findedMakeId);
            comboBox_Model.SelectedIndex = 0;
        }
    }
}