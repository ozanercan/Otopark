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
    /// Interaction logic for P_VehicleTypeList.xaml
    /// </summary>
    public partial class P_VehicleTypeList : Page, INotifyPropertyChanged
    {
        private IVehicleTypeService _vehicleTypeService;
        public P_VehicleTypeList(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
            InitializeComponent();
        }

        public List<VehicleType> VehicleTypes { get; set; }

        private void GetList()
        {
            if (VehicleTypes != null) VehicleTypes.Clear();
            VehicleTypes = _vehicleTypeService.ListByNonDeleted();
            OnPropertyChanged("VehicleTypes");
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
                case nameof(VehicleType.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;

                case nameof(VehicleType.Name):
                    e.Column.Header = Languages.LocalizationFile.VehicleTypeNameText;
                    break;

                case nameof(VehicleType.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
        }
    }
}