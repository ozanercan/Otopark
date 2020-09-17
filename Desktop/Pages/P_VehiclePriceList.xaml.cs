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
    /// Interaction logic for P_VehiclePriceList.xaml
    /// </summary>
    public partial class P_VehiclePriceList : Page, INotifyPropertyChanged
    {
        private IVehiclePriceService _vehiclePriceService;
        public P_VehiclePriceList(IVehiclePriceService vehiclePriceService)
        {
            _vehiclePriceService = vehiclePriceService;
            InitializeComponent();
        }

        public List<VehiclePriceDto> VehiclePriceDtos { get; set; }

        private void GetList()
        {
            if (VehiclePriceDtos != null) VehiclePriceDtos.Clear();
            VehiclePriceDtos = _vehiclePriceService.ListDtoByNonDeleted();
            OnPropertyChanged("VehiclePriceDtos");
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