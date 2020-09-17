using Business.Abstract;
using Business.Abstract.Apis;
using Entities.Concrete.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LicenseManager : ILicenseService
    {
        private IApiLicenseService _apiLicenseService;
        private ILocalLicenseService _localLicenseService;

        public LicenseManager(IApiLicenseService apiLicenseService, ILocalLicenseService localLicenseService)
        {
            _apiLicenseService = apiLicenseService;
            _localLicenseService = localLicenseService;
        }

        /// <summary>
        /// Lisans Kodunun Kullanılabilir Olup Olmadığnı Kontrol Eder.
        /// </summary>
        /// <returns>True kullanabilir, False kullanamaz.</returns>
        public bool CheckLicense()
        {
            string licenseCode = _localLicenseService.GetALicense().LicenseCode;
            var findedProjectLicenseCode = _apiLicenseService.Get(licenseCode);
            bool canUsed = false;
            if (findedProjectLicenseCode != null) // Geçerli bir lisans objesi bulunup geri döndürüldüyse.
            {
                canUsed = CheckAvailability(findedProjectLicenseCode);
                _localLicenseService.CreateByDeletingOldLicences(findedProjectLicenseCode);
            }
            return canUsed;
        }

        /// <summary>
        /// Verilen License Objesinin Tarih/Saat/Dakika Bazlı Kullanılabilirliğini Kontrol Eder.
        /// </summary>
        /// <returns>True ise kullanılabilir, False ise kullanılamaz</returns>
        private bool CheckAvailability(ProjectLicenseCode projectLicenseCode)
        {
            // Bitiş Tarihine Aktif tarihten itibaren süre varsa + değer döndürür
            // Eğer son kullanım tarihi geçtiyse - değer döner.
            return (projectLicenseCode.FinishedDateTime - DateTime.Now).TotalDays > 0.0001 ? true : false;
        }
    }
}
