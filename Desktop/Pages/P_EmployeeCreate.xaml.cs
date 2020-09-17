using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_EmployeeCreate.xaml
    /// </summary>
    public partial class P_EmployeeCreate : Page
    {
        private IPersonService _personService;
        private IEmployeeService _employeeService;
        public P_EmployeeCreate(IPersonService personService, IEmployeeService employeeService)
        {
            _personService = personService;
            _employeeService = employeeService;
            InitializeComponent();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = userControl_Person.GetPerson;
                int personId = _personService.Create(person);
                Employee employee = new Employee()
                {
                    PersonId = personId,
                    UserName = textBox_UserName.Text,
                    Password = passwordBox_Password.Password,
                    IsDeleted = false
                };
                _employeeService.Create(employee);
                MessageBox.Show("Personel Oluşturuldu!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}