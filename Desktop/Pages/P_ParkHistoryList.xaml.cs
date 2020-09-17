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
    /// Interaction logic for P_ParkHistoryList.xaml
    /// </summary>
    public partial class P_ParkHistoryList : Page, INotifyPropertyChanged
    {
        private IParkHistoryService _parkHistoryService;
        public P_ParkHistoryList(IParkHistoryService parkHistoryService)
        {
            _parkHistoryService = parkHistoryService;
            InitializeComponent();
        }


        public List<ParkHistoryDto> ParkHistoryDtos { get; set; }

        private void GetList()
        {
            if (ParkHistoryDtos != null) ParkHistoryDtos.Clear();
            ParkHistoryDtos = _parkHistoryService.ListDtoByNonDeleted();
            OnPropertyChanged("ParkHistoryDtos");
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
                case nameof(ParkHistoryDto.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;

                case nameof(ParkHistoryDto.ParkPlaceName):
                    e.Column.Header = Languages.LocalizationFile.ParkPlaceNameText;
                    break;

                case nameof(ParkHistoryDto.CampaignName):
                    e.Column.Header = Languages.LocalizationFile.CampaignNameText;
                    break;

                case nameof(ParkHistoryDto.Customer):
                    e.Column.Header = Languages.LocalizationFile.Customer;
                    break;

                case nameof(ParkHistoryDto.Plate):
                    e.Column.Header = Languages.LocalizationFile.Plate;
                    break;

                case nameof(ParkHistoryDto.Employee):
                    e.Column.Header = Languages.LocalizationFile.Employee;
                    break;

                case nameof(ParkHistoryDto.Price):
                    e.Column.Header = Languages.LocalizationFile.Price;
                    break;

                case nameof(ParkHistoryDto.ParkingStartDateTime):
                    e.Column.Header = Languages.LocalizationFile.ParkingStartDateTime;
                    break;

                case nameof(ParkHistoryDto.ParkedLeaveDateTime):
                    e.Column.Header = Languages.LocalizationFile.ParkedLeaveDateTime;
                    break;

                case nameof(ParkHistoryDto.CreationDateTime):
                    e.Column.Header = Languages.LocalizationFile.CreationDateTime;
                    break;

                case nameof(ParkHistoryDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }

            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = Languages.LocalizationFile.DateTimeFormat;
        }
    }
}