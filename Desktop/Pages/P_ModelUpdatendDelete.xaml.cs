using Business.Abstract;
using Core;
using Desktop.Classes;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_ModelUpdatendDelete.xaml
    /// </summary>
    public partial class P_ModelUpdatendDelete : Page, INotifyPropertyChanged
    {
        private IBrandService _brandService;
        private IModelService _modelService;
        public P_ModelUpdatendDelete(IBrandService brandService, IModelService modelService)
        {
            _brandService = brandService;
            _modelService = modelService;

            InitializeComponent();
        }

        public List<ModelDto> ModelViews { get; set; }
        public List<Brand> Brands { get; set; }
        public ModelDto SelectedModelView { get; set; }

        private void GetModelViews()
        {
            if (ModelViews != null) ModelViews.Clear();
            ModelViews = _modelService.ListDto();
            OnPropertyChanged("ModelViews");
            FillModelViewComboBox();
        }

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

        private void FillModelViewComboBox()
        {
            comboBox_Models.Items.Clear();

            foreach (var item in ModelViews)
                comboBox_Models.Items.Add(item.Model);
        }

        private int FindBrandIdByBrandName(string brandName)
        {
            int id = 0;
            foreach (var item in Brands)
            {
                if (item.Name.Equals(brandName))
                    id = item.Id;
            }
            return id;
        }

        private int FindModelIdByModelName(string modelName)
        {
            int id = 0;
            foreach (var item in ModelViews)
            {
                if (item.Model.Equals(modelName))
                    id = item.Id;
            }
            return id;
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
                GetModelViews();
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
                int findedBrandId = FindBrandIdByBrandName(comboBox_Brands.SelectedItem.ToString());
                int findedModelId = FindModelIdByModelName(comboBox_Models.SelectedItem.ToString());

                Model updatedModel = new Model()
                {
                    Id = findedModelId,
                    BrandId = findedBrandId,
                    Name = textBox_Name.Text,
                    IsDeleted = comboBox_isDelete.SelectedIndex.ToBoolean()
                };

                _modelService.Update(updatedModel);

                MessageBox.Show(string.Format(Languages.LocalizationFile.ModelUpdatedText), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);

                GetModelViews();
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
                SelectedModelView = dataGrid.SelectedItem as ModelDto;

                _brandService.Delete(SelectedModelView.Id);
                MessageBox.Show(string.Format(Languages.LocalizationFile.ModelDeletedText, SelectedModelView.Model), Languages.LocalizationFile.SuccessText, MessageBoxButton.OK, MessageBoxImage.Information);

                GetModelViews();
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
                SelectedModelView = dataGrid.SelectedItem as ModelDto;

                comboBox_Brands.Text = SelectedModelView.Brand;
                comboBox_Models.Text = SelectedModelView.Model;
                textBox_Name.Text = SelectedModelView.Model;
                comboBox_isDelete.SelectedIndex = SelectedModelView.IsDeleted.ToInt32();

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
                case nameof(ModelDto.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;

                case nameof(ModelDto.Brand):
                    e.Column.Header = Languages.LocalizationFile.Brand;
                    break;

                case nameof(ModelDto.Model):
                    e.Column.Header = Languages.LocalizationFile.Model;
                    break;

                case nameof(ModelDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
        }
    }
}