using Business.Abstract;
using Business.Abstract.Apis;
using Desktop.Classes;
using Entities.Concrete.License;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_License.xaml
    /// </summary>
    public partial class W_License : Window
    {
        private IApiLicenseService _apiLicenseService;
        private ILocalLicenseService _localLicenseService;
        public W_License(IApiLicenseService apiLicenseService, ILocalLicenseService localLicenseService)
        {
            _apiLicenseService = apiLicenseService;
            _localLicenseService = localLicenseService;
            InitializeComponent();
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProjectLicenseCode projectLicenseCodeResult = _apiLicenseService.Get(textBox_LicenseCode.Text);
                if (projectLicenseCodeResult == null)
                    MessageBox.Show("Girilen Lisans Kodu Geçersiz!", "UYARI", MessageBoxButton.OK);
                else
                {
                    _localLicenseService.Create(projectLicenseCodeResult);
                    CommonOperations.Application.Restart();
                }
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void textBox_LicenseCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            label_LicenseCodeLength.Content = $"{textBox_LicenseCode.Text.Length}/29";

            int hypenQuantity = 0;
            for (int i = 0; i < textBox_LicenseCode.Text.Length; i++)
                hypenQuantity += textBox_LicenseCode.Text.Substring(i, 1) == "-" ? 1 : 0;

            button_Confirm.IsEnabled = (hypenQuantity == 5 && textBox_LicenseCode.Text.Length == 29) ? true : false;
        }
    }
}
