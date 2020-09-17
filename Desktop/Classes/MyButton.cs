using Entities;
using Entities.Concrete;
using System.Windows.Controls;

namespace Desktop.Classes
{
    public class MyButton : Button
    {
        public MyButton()
        {
        }

        public MyButton(ParkPlace parameter)
        {
            Place = parameter;
        }

        public ParkPlace Place { get; set; }
    }
}