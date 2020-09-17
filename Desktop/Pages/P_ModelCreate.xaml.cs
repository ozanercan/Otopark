using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_ModelCreate.xaml
    /// </summary>
    public partial class P_ModelCreate : Page
    {
        private IBrandService _brandService;
        private IModelService _modelService;
        public P_ModelCreate(IBrandService brandService, IModelService modelService)
        {
            _brandService = brandService;
            _modelService = modelService;

            InitializeComponent();
        }

        public List<Brand> Brands { get; set; }
        public List<Model> Models { get; set; }

        private void ListBrands()
        {
            if (Brands != null) Brands.Clear();

            Brands = _brandService.ListByNonDeleted();
            comboBox_Brand.Items.Clear();

            foreach (var item in Brands)
                comboBox_Brand.Items.Add(item.Name);
        }

        private int FindBrandIdByBrandName(string brandName)
        {
            int findedId = 0;
            foreach (var item in Brands)
            {
                if (item.Name == brandName)
                    findedId = item.Id;
            }
            return findedId;
        }


        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model model = new Model()
                {
                    BrandId = FindBrandIdByBrandName(comboBox_Brand.SelectedItem.ToString()),
                    Name = textBox_ModelName.Text,
                    IsDeleted = false
                };
                _modelService.Create(model);
                MessageBox.Show("Model Oluşturuldu!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Information);
                textBox_ModelName.Clear();
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
                ListBrands();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}