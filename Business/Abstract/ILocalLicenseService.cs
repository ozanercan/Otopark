using Entities.Concrete.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILocalLicenseService
    {
        int Create(ProjectLicenseCode projectLicenseCode);
        void Delete(int Id);
        void DeleteAllData();
        void Update(ProjectLicenseCode projectLicenseCode);
        ProjectLicenseCode GetALicense();
        ProjectLicenseCode GetById(int Id);

        /// <summary>
        /// Kayıt ederken önce aynı Id'de bilgi olup olmadığını kontrol eder, varsa o satırı siler ve yeniden oluşturur.
        /// </summary>
        int CreateByDeletingOldLicences(ProjectLicenseCode projectLicenseCode);
    }
}
