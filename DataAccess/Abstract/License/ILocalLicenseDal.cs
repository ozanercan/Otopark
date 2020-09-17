using Entities.Concrete.License;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.License
{
    public interface ILocalLicenseDal
    {
        int Create(ProjectLicenseCode projectLicenseCode);
        void Delete(int Id);
        void Update(ProjectLicenseCode projectLicenseCode);
        ProjectLicenseCode GetALicense();
        ProjectLicenseCode GetById(int Id);

        void Truncate();

    }
}
