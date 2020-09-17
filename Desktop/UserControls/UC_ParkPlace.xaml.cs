using Business.Abstract;
using Desktop.Windows;
using Entities.Concrete;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for UC_ParkPlace.xaml
    /// </summary>
    public partial class UC_ParkPlace : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="isEditable">ParkPlaceOperations formunda düzenleme yapılırken true gönderilir. Sol tıklama özelliğini disable bırakır.</param>
        public UC_ParkPlace(bool isEditable)
        {
            InitializeComponent();
            IsEditable = isEditable;
        }

        public UC_ParkPlace(ParkPlace parkPlace, Employee employee, bool isEditable)
        {
            InitializeComponent();
            ParkPlace = parkPlace;
            _employee = employee;
            IsEditable = isEditable;
        }

        private IVehicleService _vehicleService;
        private IPersonService _personService;
        private IParkService _parkService;
        private ICustomerService _customerService;
        private IVehicleTypeService _vehicleTypeService;
        private IBrandService _brandService;
        private IModelService _modelService;
        private ICampaignService _campaignService;
        private IVehiclePriceService _vehiclePriceService;
        private IParkHistoryService _parkHistoryService;
        private Employee _employee;
        /// <summary>
        ///
        /// </summary>
        /// <param name="parkPlace"></param>
        /// <param name="employee"></param>
        /// <param name="isEditable">ParkPlaceOperations formunda düzenleme yapılırken true gönderilir. Sol tıklama özelliğini disable bırakır.</param>
        public UC_ParkPlace(IVehicleService vehicleService, IVehiclePriceService vehiclePriceService, ICampaignService campaignService, IVehicleTypeService vehicleTypeService, IBrandService brandService, IModelService modelService, IParkHistoryService parkHistoryService, IPersonService personService, IParkService parkService, ICustomerService customerService, ParkPlace parkPlace, Employee employee, bool isEditable)
        {
            _vehicleService = vehicleService;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
            _modelService = modelService;
            _campaignService = campaignService;
            _personService = personService;
            _parkService = parkService;
            _customerService = customerService;
            
            _vehiclePriceService = vehiclePriceService;
            _parkHistoryService = parkHistoryService;

            InitializeComponent();
            _employee = employee;
            IsEditable = isEditable;
            ParkPlace = parkPlace;
            OnPropertyChanged("ParkPlace");
            GeneralControl();

        }
        public ParkPlace ParkPlace { get; set; }
        public Vehicle Vehicle { get; set; }
        public Park Park { get; set; }

        /// <summary>
        /// ParkPlaceOperations formunda düzenleme yapılırken kullanılan özellik.
        /// Düzenleme yapılırken true değeri gönderilir.
        /// LeftButtonUp yapıldığında herhangi bir pencere açılmaz.
        /// </summary>
        public bool IsEditable { get; set; }



        private void ParkControl()
        {
            Park = _parkService.GetByParkPlaceIdandNonDeleted(ParkPlace.Id);
            if (Park != null)
            {
                this.Background = new SolidColorBrush(Color.FromArgb(150, 0, 171, 116));
            }
            else
            {
                this.Background = new SolidColorBrush(Color.FromArgb(150, 120, 54, 0));
            }
            OnPropertyChanged("Park");
        }

        private void VehicleControl()
        {
            if (Park != null)
            {
                Vehicle = _vehicleService.Find(Park.VehicleId.Value);
                OnPropertyChanged("Vehicle");
            }
        }

        private void GeneralControl()
        {
            if (ParkPlace != null)
                ParkControl();

            if (Park != null)
                VehicleControl();
            else
            {
                Vehicle = null;
                OnPropertyChanged("Vehicle");
            }
        }

        private void PropertyReset()
        {
            Vehicle = null;
            Park = null;
        }

        private void userControl_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void userControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.IsEditable == false)
            {
                if (Park != null) // Aktif bir park var ise -- Park Alanı Dolu İse
                {
                    W_ParkDetail w_ParkDetail = new W_ParkDetail(_parkService, _personService, _customerService, _vehicleService, _campaignService, _parkHistoryService, _vehiclePriceService, ParkPlace, _employee);
                    w_ParkDetail.ShowDialog();
                    PropertyReset();
                }
                else // Aktif bir park bulunamadıysa -- Park Alanı Boş İse
                {
                    W_ParkDetailCreate w_ParkDetailCreate = new W_ParkDetailCreate(_vehicleService, _vehicleTypeService, _brandService, _modelService, _personService, _parkService, _customerService, ParkPlace, _employee);
                    w_ParkDetailCreate.ShowDialog();
                }
                GeneralControl();
            }
        }
    }
}