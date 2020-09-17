using Business.Abstract;
using Core;
using Desktop.Classes;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_VehiclePriceUpdatendDelete.xaml
    /// </summary>
    public partial class P_VehiclePriceUpdatendDelete : Page, INotifyPropertyChanged
    {
        private IVehiclePriceService _vehiclePriceService;
        private IVehicleTypeService _vehicleTypeService;
        public P_VehiclePriceUpdatendDelete(IVehiclePriceService vehiclePriceService, IVehicleTypeService vehicleTypeService)
        {
            _vehiclePriceService = vehiclePriceService;
            _vehicleTypeService = vehicleTypeService;
            InitializeComponent();
        }


        public List<VehiclePriceDto> VehiclePriceDtos { get; set; }
        public List<VehicleType> VehicleTypes { get; set; }
        public VehiclePriceDto SelectedVehiclePriceView { get; set; }

        private void GetVehiclePrices()
        {
            if (VehiclePriceDtos != null) VehiclePriceDtos.Clear();

            VehiclePriceDtos = _vehiclePriceService.ListDto();
            OnPropertyChanged("VehiclePriceDtos");
        }

        private void GetVehicleTypes()
        {
            if (VehicleTypes != null) VehicleTypes.Clear();
            VehicleTypes = _vehicleTypeService.List();
            OnPropertyChanged("VehicleTypes");
            FillVehicleTypeComboBox();
        }

        private void FillVehicleTypeComboBox()
        {
            comboBox_VehicleType.Items.Clear();

            foreach (var item in VehicleTypes)
                comboBox_VehicleType.Items.Add(item.Name);
        }

        private int FindVehicleTypeIdByVehiclePriceView()
        {
            int result = 0;
            foreach (var item in VehicleTypes)
            {
                if (item.Name.Equals(comboBox_VehicleType.Text))
                {
                    result = item.Id;
                    break;
                }
            }
            return result;
        }

        private void GridVisiblityChange()
        {
            grid_List.Visibility = (grid_List.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
            grid_Inputs.Visibility = (grid_Inputs.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetVehiclePrices();
                GetVehicleTypes();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int vehicleTypeId = FindVehicleTypeIdByVehiclePriceView();
                VehiclePrice vehiclePrice = new VehiclePrice()
                {
                    Id = SelectedVehiclePriceView.Id,
                    VehicleTypeId = vehicleTypeId,
                    Hour = Convert.ToInt32(textBox_Hour.Text),
                    Price = Convert.ToDouble(textBox_Price.Text),
                    IsDeleted = comboBox_IsDeleted.SelectedIndex.ToBoolean()
                };

                _vehiclePriceService.Update(vehiclePrice);
                MessageBox.Show(string.Format(Languages.LocalizationFile.VehiclePriceUpdatedText), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);
                GetVehiclePrices();
                GridVisiblityChange();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void menuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedVehiclePriceView = dataGrid.SelectedItem as VehiclePriceDto;

                _vehiclePriceService.Delete(SelectedVehiclePriceView.Id);
                MessageBox.Show(string.Format(Languages.LocalizationFile.VehiclePriceDeletedText, SelectedVehiclePriceView.Id), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);

                GetVehiclePrices();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void dataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SelectedVehiclePriceView = dataGrid.SelectedItem as VehiclePriceDto;

                comboBox_VehicleType.Text = SelectedVehiclePriceView.VehicleTypeName;
                textBox_Hour.Text = SelectedVehiclePriceView.Hour.ToString();
                textBox_Price.Text = SelectedVehiclePriceView.Price.ToString();
                comboBox_IsDeleted.SelectedIndex = SelectedVehiclePriceView.IsDeleted.ToInt32();
                GridVisiblityChange();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_Previous_Click(object sender, RoutedEventArgs e)
        {
            GridVisiblityChange();
        }

        #region INotifyPropertyChanged Implementing

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Implementing

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case nameof(VehiclePriceDto.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;

                case nameof(VehiclePriceDto.VehicleTypeName):
                    e.Column.Header = Languages.LocalizationFile.VehicleTypeText;
                    break;

                case nameof(VehiclePriceDto.Hour):
                    e.Column.Header = Languages.LocalizationFile.Hour;
                    break;

                case nameof(VehiclePriceDto.Price):
                    e.Column.Header = Languages.LocalizationFile.Price;
                    break;

                case nameof(VehiclePriceDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
        }
    }
}