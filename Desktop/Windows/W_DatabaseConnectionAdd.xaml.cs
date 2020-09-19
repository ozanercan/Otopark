using Business.Abstract;
using Desktop.Classes;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_DatabaseConnectionAdd.xaml
    /// </summary>
    public partial class W_DatabaseConnectionAdd : Window
    {
        private IDatabaseConnectionOperationService _databaseConnectionTestService;
        public W_DatabaseConnectionAdd(IDatabaseConnectionOperationService databaseConnectionTestService)
        {
            _databaseConnectionTestService = databaseConnectionTestService;
            InitializeComponent();
        }

        private DatabaseConnectionInformation GenerateDatabaseInformation()
        {
            return new DatabaseConnectionInformation()
            {
                Hostname = textBox_HostName.Text,
                Database = textBox_Database.Text,
                Username = textBox_Username.Text,
                Password = password_Password.Password
            };
        }
        private void SaveConnectionString()
        {
            string generatedConnectionString = _databaseConnectionTestService.GenerateConnectionString(GenerateDatabaseInformation());
            _databaseConnectionTestService.SetConnectionString(generatedConnectionString);
        }
        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveConnectionString();
            MessageBox.Show("Program Yeniden Başlatılıyor!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Warning);
            CommonOperations.Application.Restart();
        }

        private void button_Test_Click(object sender, RoutedEventArgs e)
        {
            bool isConnectionOpened = _databaseConnectionTestService.Test(GenerateDatabaseInformation());

            if (!isConnectionOpened)
            {
                MessageBox.Show("Bağlantı Sağlanamadı!", "UYARI", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (MessageBox.Show("Bağlantı Sağlandı! Bilgilerin Kayıt Edilmesini İstiyor Musunuz?", "UYARI", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    button_Save_Click(sender, e);
                    this.Close();
                }
            }
        }
    }
}
