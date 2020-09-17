using Entities.Concrete.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.Apis
{
    public interface IApiLicenseService
    {
        /// <summary>
        /// Verilen lisans koduna göre kullanılabilirliği kontrol eder.
        /// </summary>
        /// <param name="license">Lisans Kodu</param>
        /// <returns>Eğer lisans kullanılabilirse true değeri döner.</returns>
        ProjectLicenseCode Get(string license);
    }
}
