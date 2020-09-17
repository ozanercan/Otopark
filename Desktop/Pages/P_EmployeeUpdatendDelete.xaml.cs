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
    /// Interaction logic for P_EmployeeUpdatendDelete.xaml
    /// </summary>
    public partial class P_EmployeeUpdatendDelete : Page, INotifyPropertyChanged
    {
        private IPersonService _personService;
        private IEmployeeService _employeeService;
        public P_EmployeeUpdatendDelete(IPersonService personService, IEmployeeService employeeService)
        {
            _personService = personService;
            _employeeService = employeeService;
            InitializeComponent();
        }

        public List<EmployeeDto> EmployeeDtos { get; set; }
        public EmployeeDto SelectedEmployeeDto { get; set; }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            GetList();
        }

        private void GetList()
        {
            if (EmployeeDtos != null) EmployeeDtos.Clear();
            EmployeeDtos = _employeeService.ListDto();
            OnPropertyChanged("EmployeeDtos");
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

        private void dataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SelectedEmployeeDto = dataGrid.SelectedItem as EmployeeDto;

                userControl_Person.SetPerson(new Person()
                {
                    Id = SelectedEmployeeDto.Id,
                    FirstName = SelectedEmployeeDto.FirstName,
                    LastName = SelectedEmployeeDto.LastName,
                    NationalIdentityNumber = SelectedEmployeeDto.NationalIdentityNumber,
                    TelephoneNumber = SelectedEmployeeDto.TelephoneNumber,
                    CreationDate = SelectedEmployeeDto.CreationDate
                });

                textBox_UserName.Text = SelectedEmployeeDto.UserName;
                textBox_Password.Text = SelectedEmployeeDto.Password;
                comboBox_IsDeleted.SelectedIndex = SelectedEmployeeDto.IsDeleted.ToInt32();
                GridVisibilityChange();
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GridVisibilityChange()
        {
            grid_Inputs.Visibility = (grid_Inputs.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
            grid_List.Visibility = (grid_List.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee employee = _employeeService.Find(SelectedEmployeeDto.Id);
                int personId = _personService.Find(employee.PersonId).Id;

                Person person = userControl_Person.GetPerson;
                person.Id = personId;
                _personService.Update(person);

                Employee newEmployee = new Employee()
                {
                    Id = SelectedEmployeeDto.Id,
                    PersonId = personId,
                    UserName = textBox_UserName.Text,
                    Password = textBox_Password.Text,
                    IsDeleted = comboBox_IsDeleted.SelectedIndex.ToBoolean()
                };

                _employeeService.Update(newEmployee);

                GetList();

                MessageBox.Show(Languages.LocalizationFile.EmployeeUpdateText, Languages.LocalizationFile.WarningText, MessageBoxButton.OK, MessageBoxImage.Information);

                GridVisibilityChange();
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
                SelectedEmployeeDto = dataGrid.SelectedItem as EmployeeDto;
                _employeeService.Delete(SelectedEmployeeDto.Id);
                MessageBox.Show(Languages.LocalizationFile.EmployeeDeleteText, Languages.LocalizationFile.WarningText, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
                    (e.Column as DataGridTextColumn).Binding.StringFormat = Languages.LocalizationFile.DateFormat;
                    break;

                case nameof(EmployeeDto.LastLoginDateTime):
                    e.Column.Header = Languages.LocalizationFile.LastLoginDateTime;
                    (e.Column as DataGridTextColumn).Binding.StringFormat = Languages.LocalizationFile.DateTimeFormat;
                    break;

                case nameof(EmployeeDto.IsDeleted):
                    e.Column.Header = Languages.LocalizationFile.IsDelete;
                    break;
            }
        }
    }
}