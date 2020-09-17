using Business.Abstract;
using Core;
using Desktop.Classes;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_VehicleTypeUpdatendDelete.xaml
    /// </summary>
    public partial class P_VehicleUpdatendDelete : Page, INotifyPropertyChanged
    {
        private IVehicleService _vehicleService;
        private IVehicleTypeService _vehicleTypeService;
        private IEmployeeService _employeeService;
        private IBrandService _brandService;
        private IModelService _modelService;
        public P_VehicleUpdatendDelete(IVehicleService vehicleService, IVehicleTypeService vehicleTypeService, IEmployeeService employeeService, IBrandService brandService, IModelService modelService)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _employeeService = employeeService;
            _brandService = brandService;
            _modelService = modelService;
            InitializeComponent();

        }

        public List<VehicleDto> VehicleDtos { get; set; }
        private VehicleDto SelectedVehicleView { get; set; }
        private Vehicle SelectedVehicle { get; set; }
        private List<Person> Persons { get; set; }
        private List<VehicleType> VehicleTypes { get; set; }
        private List<Brand> Brands { get; set; }
        private List<Model> Models { get; set; }

        private void GetVehicles()
        {
            if (VehicleDtos != null) VehicleDtos.Clear();
            VehicleDtos = _vehicleService.ListDto();
            OnPropertyChanged("VehicleDtos");
        }
        private void GetBrands()
        {
            if (Brands != null)
            {
                Brands.Clear();
                comboBox_Brands.Items.Clear();
            }
            Brands = _brandService.ListByNonDeleted();
            OnPropertyChanged("Brands");

            foreach (var item in Brands)
                comboBox_Brands.Items.Add(item.Name);
        }
        private void GetModels(int brandId)
        {
            if (Models != null)
            {
                Models.Clear();
                comboBox_Models.Items.Clear();
            }
            Models = _modelService.ListByBrandIdAndNonDeleted(brandId);
            OnPropertyChanged("Models");

            foreach (var item in Models)
                comboBox_Models.Items.Add(item.Name);
        }

        private void menuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedVehicleView = dataGrid.SelectedItem as VehicleDto;
                if (MessageBox.Show(String.Format(Languages.LocalizationFile.VehicleDeleteQuestionText, SelectedVehicleView.Plate), Languages.LocalizationFile.QuestionText, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _vehicleService.ChangeIsDeletedByVehicleId(SelectedVehicleView.Id);
                    GetVehicles();
                    MessageBox.Show(String.Format(Languages.LocalizationFile.VehicleDeletedText, SelectedVehicleView.Plate), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void GridVisibilityChange()
        {
            grid_Inputs.Visibility = (grid_Inputs.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
            grid_List.Visibility = (grid_List.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void dataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SelectedVehicleView = dataGrid.SelectedItem as VehicleDto;

                SelectedVehicle = _vehicleService.Find(SelectedVehicleView.Id);

                Persons = _employeeService.FindAllByPersonInformation();

                comboBox_Employee.Items.Clear();
                foreach (var item in Persons)
                {
                    comboBox_Employee.Items.Add($"{item.FirstName} {item.LastName}");
                }

                VehicleTypes = _vehicleTypeService.List();

                comboBox_VehicleType.Items.Clear();
                foreach (var item in VehicleTypes)
                {
                    comboBox_VehicleType.Items.Add(item.Name);
                }

                comboBox_VehicleType.Text = SelectedVehicleView.VehicleType;
                comboBox_Employee.Text = SelectedVehicleView.Employee;
                textBox_Plate.Text = SelectedVehicleView.Plate;
                comboBox_Brands.Text = SelectedVehicleView.Brand;
                comboBox_Models.Text = SelectedVehicleView.Model;
                textBox_Color.Text = SelectedVehicleView.Color;
                comboBox_IsDeleted.SelectedIndex = SelectedVehicleView.IsDeleted.ToInt32();
                GridVisibilityChange();
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

        private void button_Previous_Click(object sender, RoutedEventArgs e)
        {
            GridVisibilityChange();
        }

        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int vehicleTypeId = _vehicleTypeService.FindIdByVehicleName(VehicleTypes, comboBox_VehicleType.Text);
                // Todo: Düzeltilecek
                _vehicleService.Update(new Vehicle()
                {
                    Id = SelectedVehicleView.Id,
                    VehicleTypeId = vehicleTypeId,
                    EmployeeId = SelectedVehicle.EmployeeId,
                    Plate = textBox_Plate.Text,
                    BrandId = Brands.Where(i => i.Name == comboBox_Brands.Text).FirstOrDefault().Id,
                    ModelId = Models.Where(i => i.Name == comboBox_Models.Text).FirstOrDefault().Id,
                    Color = textBox_Color.Text,
                    IsDeleted = comboBox_IsDeleted.SelectedIndex.ToBoolean()
                });
                GetVehicles();
                MessageBox.Show(String.Format(Languages.LocalizationFile.VehicleUpdatedText, SelectedVehicleView.Plate), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);
                GridVisibilityChange();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case nameof(VehicleDto.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;

                case nameof(VehicleDto.VehicleType):
                    e.Column.Header = Languages.LocalizationFile.VehicleTypeText;
                    break;

                case nameof(VehicleDto.Employee):
                    e.Column.Header = Languages.LocalizationFile.Employee;
                    break;

                case nameof(VehicleDto.Plate):
                    e.Column.Header = Languages.LocalizationFile.Plate;
                    break;

                case nameof(VehicleDto.Brand):
                    e.Column.Header = Languages.LocalizationFile.Brand;
                    break;

                case nameof(VehicleDto.Model):
                    e.Column.Header = Languages.LocalizationFile.Model;
                    break;

                case nameof(VehicleDto.Color):
                    e.Column.Header = Languages.LocalizationFile.Color;
                    break;

                case nameof(VehicleDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetVehicles();
                GetBrands();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void comboBox_Brands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GetModels(Brands.Where(i => i.Name == comboBox_Brands.Text).FirstOrDefault().Id);
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}