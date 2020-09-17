using Business.Abstract;
using Core;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_VehicleTypeUpdatendDelete.xaml
    /// </summary>
    public partial class P_VehicleTypeUpdatendDelete : Page, INotifyPropertyChanged
    {
        private IVehicleTypeService _vehicleTypeService;
        public P_VehicleTypeUpdatendDelete(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
            InitializeComponent();

            GetVehicleTypes();
        }


        public List<VehicleType> VehicleTypes { get; set; }
        private VehicleType SelectedVehicleType { get; set; }

        private void GetVehicleTypes()
        {
            if (VehicleTypes != null)
                VehicleTypes.Clear();
            VehicleTypes = _vehicleTypeService.List();
            OnPropertyChanged("VehicleTypes");
        }


        private void menuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedVehicleType = dataGrid.SelectedItem as VehicleType;

                if (MessageBox.Show(String.Format(Languages.LocalizationFile.VehicleTypeDeleteQuestionText, SelectedVehicleType.Name), Languages.LocalizationFile.QuestionText, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _vehicleTypeService.ChangeIsDeletedByVehicleTypeId(SelectedVehicleType.Id);
                    GetVehicleTypes();
                    MessageBox.Show(String.Format(Languages.LocalizationFile.VehicleTypeDeletedText, SelectedVehicleType.Name), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _vehicleTypeService.Update(new VehicleType()
                {
                    Id = SelectedVehicleType.Id,
                    Name = textBox_Name.Text,
                    IsDeleted = comboBox_isDeleted.SelectedIndex.ToBoolean()
                });
                GetVehicleTypes();
                MessageBox.Show(String.Format(Languages.LocalizationFile.VehicleTypeUpdatedText, SelectedVehicleType.Name), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);
                GridVisibilityChange();
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
                SelectedVehicleType = dataGrid.SelectedItem as VehicleType;
                textBox_Name.Text = SelectedVehicleType.Name;
                comboBox_isDeleted.SelectedIndex = SelectedVehicleType.IsDeleted.ToInt32();
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