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
    /// Interaction logic for P_ParkList.xaml
    /// </summary>
    public partial class P_ParkList : Page, INotifyPropertyChanged
    {
        private IParkService _parkService;
        public P_ParkList(IParkService parkService)
        {
            _parkService = parkService;

            InitializeComponent();
        }


        public List<ParkDto> ParkDtos { get; set; }

        private void GetList()
        {
            if (ParkDtos != null) ParkDtos.Clear();
            ParkDtos = _parkService.ListDtoByNonDeleted();
            OnPropertyChanged("ParkDtos");
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Implementing

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case nameof(ParkDto.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;

                case nameof(ParkDto.ParkPlaceName):
                    e.Column.Header = Languages.LocalizationFile.ParkPlaceNameText;
                    break;

                case nameof(ParkDto.CampaignName):
                    e.Column.Header = Languages.LocalizationFile.CampaignNameText;
                    break;

                case nameof(ParkDto.Customer):
                    e.Column.Header = Languages.LocalizationFile.Customer;
                    break;

                case nameof(ParkDto.Plate):
                    e.Column.Header = Languages.LocalizationFile.Plate;
                    break;

                case nameof(ParkDto.Employee):
                    e.Column.Header = Languages.LocalizationFile.Employee;
                    break;

                case nameof(ParkDto.ParkingStartDateTime):
                    e.Column.Header = Languages.LocalizationFile.ParkingStartDateTime;
                    break;

                case nameof(ParkDto.CreationDateTime):
                    e.Column.Header = Languages.LocalizationFile.CreationDateTime;
                    break;

                case nameof(ParkDto.IsState):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }

            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = Languages.LocalizationFile.DateTimeFormat;
        }
    }
}