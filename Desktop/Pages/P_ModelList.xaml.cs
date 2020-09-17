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
    /// Interaction logic for P_ModelList.xaml
    /// </summary>
    public partial class P_ModelList : Page, INotifyPropertyChanged
    {
        private IModelService _modelService;
        public P_ModelList(IModelService modelService)
        {
            _modelService = modelService;

            InitializeComponent();
        }

        public List<ModelDto> ModelDtos { get; set; }

        private void GetModelDtos()
        {
            if (ModelDtos != null) ModelDtos.Clear();
            ModelDtos = _modelService.ListDtoByNonDeleted();
            OnPropertyChanged("ModelDtos");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetModelDtos();
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