using Entities.Concrete;
using Entities.Concrete.Models;

namespace DataAccess.Abstract
{
    public interface IEmployeeDal : IBasicMethods<Employee>, IDeleteEntity<Employee>
    {
        /// <summary>
        /// Kullanıcı adı ve şifreye göre bilgileri kontrol edip geriye değer döndürür.
        /// </summary>
        /// <returns>Giriş bilgileri onaylanırsa true değeri döner.</returns>
        bool LoginControl(Login parameter);


        Employee FindEmployeeByUsernamendPassword(Login parameter);
    }
}