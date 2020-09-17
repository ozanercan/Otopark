using Business.Abstract;
using Core;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_BrandUpdatendDelete.xaml
    /// </summary>
    public partial class P_BrandUpdatendDelete : Page, INotifyPropertyChanged
    {
        private IBrandService _brandService;
        public P_BrandUpdatendDelete(IBrandService brandService)
        {
            _brandService = brandService;
            InitializeComponent();
        }


        public List<Brand> Brands { get; set; }
        public Brand SelectedBrand { get; set; }

        private void GetBrands()
        {
            if (Brands != null) Brands.Clear();
            Brands = _brandService.List();
            OnPropertyChanged("Brands");
            FillBrandComboBox();
        }

        private void FillBrandComboBox()
        {
            comboBox_Brands.Items.Clear();

            foreach (var item in Brands)
                comboBox_Brands.Items.Add(item.Name);
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
                GetBrands();
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
                int findedBrandId = Brands.Where(i => i.Name.Equals(comboBox_Brands.SelectedItem.ToString())).FirstOrDefault().Id;

                Brand updatedBrand = new Brand()
                {
                    Id = findedBrandId,
                    Name = textBox_Name.Text,
                    IsDeleted = comboBox_isDelete.SelectedIndex.ToBoolean()
                };

                _brandService.Update(updatedBrand);

                MessageBox.Show(string.Format(Languages.LocalizationFile.BrandUpdatedText), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);

                GetBrands();
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
                SelectedBrand = dataGrid.SelectedItem as Brand;

                _brandService.ChangeIsDeletedByBrandId(SelectedBrand.Id);
                MessageBox.Show(string.Format(Languages.LocalizationFile.BrandDeletedText, SelectedBrand.Id), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);

                GetBrands();
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
                SelectedBrand = dataGrid.SelectedItem as Brand;

            comboBox_Brands.Text = SelectedBrand.Name;
            textBox_Name.Text = SelectedBrand.Name;
            comboBox_isDelete.SelectedIndex = SelectedBrand.IsDeleted.ToInt32();

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
                case nameof(Brand.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;
                case nameof(Brand.Name):
                    e.Column.Header = Languages.LocalizationFile.Brand;
                    break;
                case nameof(Brand.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
        }
    }
}