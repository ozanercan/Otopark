using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete;
using Languages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for P_CustomerCreate.xaml
    /// </summary>
    public partial class P_CustomerCreate : Page
    {

        private IPersonService _personService;
        private ICustomerService _customerService;
        public P_CustomerCreate(IPersonService personService, ICustomerService customerService)
        {
            _personService = personService;
            _customerService = customerService;
            InitializeComponent();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = userControl_Person.GetPerson;
                int personId = _personService.Create(person);
                Customer customer = new Customer()
                {
                    PersonId = personId,
                    IsDeleted = false
                };
                _customerService.Create(customer);
                MessageBox.Show(LocalizationFile.CustomerCreateText, LocalizationFile.WarningText, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log4net.log.Error("Error: ", ex);
                MessageBox.Show(ex.Message, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}