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
    /// Interaction logic for P_CustomerUpdatendDelete.xaml
    /// </summary>
    public partial class P_CustomerUpdatendDelete : Page, INotifyPropertyChanged
    {
        private Employee _employee;
        private IPersonService _personService;
        private ICustomerService _customerService;
        public P_CustomerUpdatendDelete(ICustomerService customerService, IPersonService personService, Employee employee)
        {
            _customerService = customerService;
            _personService = personService;
            _employee = employee;
            InitializeComponent();
        }


        public List<CustomerDto> CustomerDtos { get; set; }
        public CustomerDto SelectedCustomerDto { get; set; }

        private int FindPersonIdByEmployeeId()
        {
            return _customerService.Find(SelectedCustomerDto.Id).PersonId;
        }

        public void GetList()
        {
            if (CustomerDtos != null) CustomerDtos.Clear();
            CustomerDtos = _customerService.ListDto();
            OnPropertyChanged("CustomerDtos");
        }

        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int personId = FindPersonIdByEmployeeId();

                Person newPerson = userControl_Person.GetPerson;
                newPerson.Id = personId;

                Customer newCustomer = new Customer()
                {
                    Id = SelectedCustomerDto.Id,
                    PersonId = personId,
                    IsDeleted = comboBox_IsDeleted.SelectedIndex.ToBoolean()
                };

                _personService.Update(newPerson);
                _customerService.Update(newCustomer);
                MessageBox.Show(Languages.LocalizationFile.CustomerUpdateText, Languages.LocalizationFile.WarningText, MessageBoxButton.OK, MessageBoxImage.Information);
                GetList();
                GridVisiblityChange();
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
                SelectedCustomerDto = dataGrid.SelectedItem as CustomerDto;

                userControl_Person.SetPerson(new Person()
                {
                    FirstName = SelectedCustomerDto.FirstName,
                    LastName = SelectedCustomerDto.LastName,
                    NationalIdentityNumber = SelectedCustomerDto.NationalIdentityNumber,
                    TelephoneNumber = SelectedCustomerDto.TelephoneNumber,
                    CreationDate = SelectedCustomerDto.CreationDate
                });
                comboBox_IsDeleted.SelectedIndex = SelectedCustomerDto.IsDeleted.ToInt32();
                GridVisiblityChange();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GridVisiblityChange()
        {
            grid_List.Visibility = (grid_List.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
            grid_Inputs.Visibility = (grid_Inputs.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void menuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int personId = FindPersonIdByEmployeeId();
                _personService.Delete(personId);
                MessageBox.Show(Languages.LocalizationFile.CustomerDeleteText, Languages.LocalizationFile.WarningText, MessageBoxButton.OK, MessageBoxImage.Information);
                GetList();
                GridVisiblityChange();
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

        private void page_Loaded(object sender, RoutedEventArgs e)
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

        private void button_Previous_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridVisiblityChange();
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case nameof(EmployeeDto.Id):
                    e.Column.Header = Languages.LocalizationFile.Id;
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

                case nameof(EmployeeDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = Languages.LocalizationFile.DateFormat;
        }
    }
}