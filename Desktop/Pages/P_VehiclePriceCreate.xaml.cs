using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_VehiclePriceCreate.xaml
    /// </summary>
    public partial class P_VehiclePriceCreate : Page, INotifyPropertyChanged
    {
        private IVehicleTypeService _vehicleTypeService;
        private IVehiclePriceService _vehiclePriceService;
        public P_VehiclePriceCreate(IVehicleTypeService vehicleTypeService, IVehiclePriceService vehiclePriceService)
        {
            _vehicleTypeService = vehicleTypeService;
            _vehiclePriceService = vehiclePriceService;
            InitializeComponent();
        }

        public List<VehicleType> VehicleTypes { get; set; }

        private void GetVehicleTypes()
        {
            if (VehicleTypes != null) VehicleTypes.Clear();
            VehicleTypes = _vehicleTypeService.ListByNonDeleted();
            OnPropertyChanged("VehicleTypes");
            FillVehicleTypeComboBox();
        }

        private void FillVehicleTypeComboBox()
        {
            comboBox_VehicleType.Items.Clear();
            foreach (var item in VehicleTypes)
            {
                comboBox_VehicleType.Items.Add(item.Name);
            }
        }

        private int FindVehicleTypeIdByVehicleTypes()
        {
            int result = -1;
            foreach (var item in VehicleTypes)
            {
                if (item.Name.Equals(comboBox_VehicleType.Text))
                {
                    return item.Id;
                }
            }
            return result;
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int vehicleTypeId = FindVehicleTypeIdByVehicleTypes();
                VehiclePrice vehiclePrice = new VehiclePrice()
                {
                    VehicleTypeId = vehicleTypeId,
                    Hour = Convert.ToInt32(textBox_Hour.Text),
                    Price = Convert.ToDouble(textBox_Price.Text),
                    IsDeleted = false
                };

                _vehiclePriceService.Create(vehiclePrice);
                MessageBox.Show(Languages.LocalizationFile.VehiclePriceCreatedText, Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetVehicleTypes();
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Implementing
    }
}