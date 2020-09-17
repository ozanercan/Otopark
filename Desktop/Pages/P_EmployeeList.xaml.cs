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
    /// Interaction logic for P_VehicleTypeList.xaml
    /// </summary>
    public partial class P_EmployeeList : Page, INotifyPropertyChanged
    {
        private IEmployeeService _employeeService;
        public P_EmployeeList(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            InitializeComponent();
        }

        public List<EmployeeDto> EmployeeDtos { get; set; }

        private void GetList()
        {
            if (EmployeeDtos != null) EmployeeDtos.Clear();
            EmployeeDtos = _employeeService.ListDtoByNonDeleted();
            OnPropertyChanged("EmployeeDtos");
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
                case nameof(EmployeeDto.UserName):
                    e.Column.Header = Languages.LocalizationFile.UserName;
                    break;

                case nameof(EmployeeDto.Password):
                    e.Column.Header = Languages.LocalizationFile.Password;
                    break;

                case nameof(EmployeeDto.NationalIdentityNumber):
                    e.Column.Header = Languages.LocalizationFile.NationalIdentityNumber;
                    break;

                case nameof(EmployeeDto.FirstName):
                    e.Column.Header = Languages.LocalizationFile.FirstName;
                    break;

                case nameof(EmployeeDto.LastName):
                    e.Column.Header = Languages.LocalizationFile.LastName;
                    break;

                case nameof(EmployeeDto.TelephoneNumber):
                    e.Column.Header = Languages.LocalizationFile.TelephoneNumber;
                    break;

                case nameof(EmployeeDto.CreationDate):
                    e.Column.Header = Languages.LocalizationFile.CreationDate;
                    break;

                case nameof(EmployeeDto.LastLoginDateTime):
                    e.Column.Header = Languages.LocalizationFile.LastLoginDateTime;
                    break;

                case nameof(EmployeeDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
            if (e.PropertyType == typeof(DateTime) || e.PropertyType == typeof(DateTime?))
                (e.Column as DataGridTextColumn).Binding.StringFormat = Languages.LocalizationFile.DateTimeFormat;
        }
    }
}