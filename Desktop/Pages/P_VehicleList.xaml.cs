using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_VehicleList.xaml
    /// </summary>
    public partial class P_VehicleList : Page, INotifyPropertyChanged
    {
        private IVehicleService _vehicleService;
        public P_VehicleList(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            InitializeComponent();
        }

        public List<VehicleDto> VehicleDtos { get; set; }

        private void GetList()
        {
            if (VehicleDtos != null) VehicleDtos.Clear();
            VehicleDtos = _vehicleService.ListDtoByNonDeleted();
            OnPropertyChanged("VehicleDtos");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetList();
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
    }
}