using Business.Abstract;
using Business.Concrete;
using Entities;
using Mobile.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        IEmployeeService employeeService = new EmployeeService();
        private void Button_Clicked(object sender, EventArgs e)
        {
            Login login = new Login()
            {
                UserName = entry_UserName.Text,
                Password = entry_Password.Text
            };

            Employee employee = employeeService.FindEmployeeByUsernamendPassword(login);
            if (employee == null)
            {
                DisplayAlert("BAŞARISIZ", "Kullanıcı adı veya şifre yanlış!", "İptal");
            }
            else
            {
                Navigation.PushModalAsync(new ParkPlaceCreate());
            }
        }
    }
}
