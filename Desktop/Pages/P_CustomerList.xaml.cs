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
    /// Interaction logic for P_CustomerList.xaml
    /// </summary>
    public partial class P_CustomerList : Page, INotifyPropertyChanged
    {
        private ICustomerService _customerService;
        public P_CustomerList(ICustomerService customerService)
        {
            _customerService = customerService;
            InitializeComponent();
        }

        public List<CustomerDto> CustomerDtos { get; set; }

        private void GetList()
        {
            if (CustomerDtos != null) CustomerDtos.Clear();
            CustomerDtos = _customerService.ListDtoByNonDeleted();
            OnPropertyChanged("CustomerDtos");
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
                case nameof(CustomerDto.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
                    break;

                case nameof(CustomerDto.NationalIdentityNumber):
                    e.Column.Header = Languages.LocalizationFile.NationalIdentityNumber;
                    break;

                case nameof(CustomerDto.FirstName):
                    e.Column.Header = Languages.LocalizationFile.FirstName;
                    break;

                case nameof(CustomerDto.LastName):
                    e.Column.Header = Languages.LocalizationFile.LastName;
                    break;

                case nameof(CustomerDto.TelephoneNumber):
                    e.Column.Header = Languages.LocalizationFile.TelephoneNumber;
                    break;

                case nameof(CustomerDto.CreationDate):
                    e.Column.Header = Languages.LocalizationFile.CreationDate;
                    break;

                case nameof(CustomerDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = Languages.LocalizationFile.DateFormat;
        }
    }
}