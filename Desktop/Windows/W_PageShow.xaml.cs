using System.Windows;
using System.Windows.Controls;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for W_PageShow.xaml
    /// </summary>
    public partial class W_PageShow : Window
    {
        public W_PageShow(Page page)
        {
            InitializeComponent();
            frame.Navigate(page);
            this.Title = page.Title;
        }
    }
}