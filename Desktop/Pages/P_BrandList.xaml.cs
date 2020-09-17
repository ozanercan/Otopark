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
    /// Interaction logic for P_BrandList.xaml
    /// </summary>
    public partial class P_BrandList : Page, INotifyPropertyChanged
    {
        private IBrandService _brandService;
        public P_BrandList(IBrandService brandService)
        {
            _brandService = brandService;
            InitializeComponent();
        }

        public List<Brand> Brands { get; set; }

        private void GetList()
        {
            if (Brands != null) Brands.Clear();
            Brands = _brandService.ListByNonDeleted();
            OnPropertyChanged("Brands");
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