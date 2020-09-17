using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_BrandCreate.xaml
    /// </summary>
    public partial class P_BrandCreate : Page
    {
        private IBrandService _brandService;
        public P_BrandCreate(IBrandService brandService)
        {
            _brandService = brandService;
            InitializeComponent();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            Brand brand = new Brand()
            {
                Name = textBox_BrandName.Text,
                IsDeleted = false
            };

            try
            {
                _brandService.Create(brand);
                MessageBox.Show("Marka Oluşturuldu!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Information);
                textBox_BrandName.Clear();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}