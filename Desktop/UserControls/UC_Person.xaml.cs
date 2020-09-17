using Entities;
using Entities.Concrete;
using System;
using System.Windows.Controls;

namespace Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for UC_Person.xaml
    /// </summary>
    public partial class UC_Person : UserControl
    {
        public UC_Person()
        {
            InitializeComponent();
        }

        public void SetPerson(Person value)
        {
            textBox_FirstName.Text = value.FirstName;
            textBox_LastName.Text = value.LastName;
            textBox_NationalIdentityNumber.Text = value.NationalIdentityNumber;
            textBox_TelephoneNumber.Text = value.TelephoneNumber;
        }

        public Person GetPerson
        {
            get
            {
                return new Person()
                {
                    FirstName = textBox_FirstName.Text,
                    LastName = textBox_LastName.Text,
                    NationalIdentityNumber = textBox_NationalIdentityNumber.Text,
                    TelephoneNumber = textBox_TelephoneNumber.Text,
                    CreationDate = DateTime.Now
                };
            }
        }
    }
}