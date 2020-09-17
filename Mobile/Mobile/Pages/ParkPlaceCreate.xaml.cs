using Business.Abstract;
using Business.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParkPlaceCreate : ContentPage
    {
        public ParkPlaceCreate()
        {
            InitializeComponent();
            ParkPlaces = parkPlaceService.List();

            listView.ItemsSource = ParkPlaces;
        }

        IParkPlaceService parkPlaceService = new ParkPlaceService();

        public List<ParkPlace> ParkPlaces { get; set; }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}