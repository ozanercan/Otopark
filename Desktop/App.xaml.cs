using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using DataAccess.Concrete.SQLite;
using Desktop.Classes;
using Desktop.Properties;
using Desktop.Windows;
using System.Data.SQLite;
using System.Windows;
using Unity;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            // Servisler
            container.RegisterType<IBrandService, BrandManager>();
            container.RegisterType<ICampaignService, CampaignManager>();
            container.RegisterType<ICustomerService, CustomerManager>();
            container.RegisterType<IEmployeeService, EmployeeManager>();
            container.RegisterType<IModelService, ModelManager>();
            container.RegisterType<IParkHistoryService, ParkHistoryManager>();
            container.RegisterType<IParkService, ParkManager>();
            container.RegisterType<IParkPlaceService, ParkPlaceManager>();
            container.RegisterType<IPersonService, PersonManager>();
            container.RegisterType<IVehicleService, VehicleManager>();
            container.RegisterType<IVehiclePriceService, VehiclePriceManager>();
            container.RegisterType<IVehicleTypeService, VehicleTypeManager>();
            container.RegisterType<IDatabaseConnectionOperationService, DatabaseConnectionOperationManager>();

            // Api Servisi


            // Data Access Layer
            container.RegisterType<IBrandDal, MySqlBrandDal>();
            container.RegisterType<ICampaignDal, MySqlCampaignDal>();
            container.RegisterType<ICustomerDal, MySqlCustomerDal>();
            container.RegisterType<IEmployeeDal, MySqlEmployeeDal>();
            container.RegisterType<IModelDal, MySqlModelDal>();
            container.RegisterType<IParkHistoryDal, MySqlParkHistoryDal>();
            container.RegisterType<IParkDal, MySqlParkDal>();
            container.RegisterType<IParkPlaceDal, MySqlParkPlaceDal>();
            container.RegisterType<IPersonDal, MySqlPersonDal>();
            container.RegisterType<IVehicleDal, MySqlVehicleDal>();
            container.RegisterType<IVehiclePriceDal, MySqlVehiclePriceDal>();
            container.RegisterType<IVehicleTypeDal, MySqlVehicleTypeDal>();

            container.RegisterType<IDatabaseConnectionTestItem, MySqlConnectionTestDal>();
            container.RegisterType<IDatabaseConnectionStringDal, SqliteDatabaseConnectionStringDal>();



            //W_Login w_Login = container.Resolve<W_Login>();
            //w_Login.Show();
            W_Welcome w_Welcome = container.Resolve<W_Welcome>();
            w_Welcome.Show();
        }
    }
}