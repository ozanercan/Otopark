using Business.Abstract;
using Desktop.Pages;
using Entities.Concrete;
using System.Windows;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_CustomerCreate.xaml
    /// </summary>
    public partial class W_CustomerCreate : Window
    {
        private IPersonService _personService;
        private ICustomerService _customerService;
        private Employee _employee;

        public W_CustomerCreate(IPersonService personService, ICustomerService customerService, Employee employee)
        {
            _employee = employee;
            _personService = personService;
            _customerService = customerService;
            InitializeComponent();
        }

        public string PersonNationalityNumber { get; set; }

        private P_CustomerCreate p_CustomerCreate;

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            p_CustomerCreate = new P_CustomerCreate(_personService, _customerService);
            p_CustomerCreate.userControl_Person.textBox_NationalIdentityNumber.Text = PersonNationalityNumber;
            frame.Navigate(p_CustomerCreate);
        }
    }
}