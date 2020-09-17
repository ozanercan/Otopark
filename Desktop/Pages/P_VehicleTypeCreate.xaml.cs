using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_VehicleTypeCreate.xaml
    /// </summary>
    public partial class P_VehicleTypeCreate : Page
    {
        private IVehicleTypeService _vehicleTypeService;
        public P_VehicleTypeCreate(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
            InitializeComponent();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VehicleType vehicleType = new VehicleType()
                {
                    Name = textBox_Name.Text,
                    IsDeleted = false
                };
                _vehicleTypeService.Create(vehicleType);
                MessageBox.Show(Languages.LocalizationFile.VehicleTypeCreateText, Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
               Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            
        }
    }
}