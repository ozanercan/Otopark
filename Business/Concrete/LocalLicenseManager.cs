using Business.Abstract;
using DataAccess.Abstract.License;
using Entities.Concrete.License;
using System;

namespace Business.Concrete
{
    public class LocalLicenseManager : ILocalLicenseService
    {
        private ILocalLicenseDal _localLicenseDal;
        public LocalLicenseManager(ILocalLicenseDal localLicenseDal)
        {
            _localLicenseDal = localLicenseDal;
        }
        public int Create(ProjectLicenseCode projectLicenseCode)
        {
            return _localLicenseDal.Create(projectLicenseCode);
        }

        public int CreateByDeletingOldLicences(ProjectLicenseCode projectLicenseCode)
        {
            int result = 0;
            if (projectLicenseCode != null)
            {
                this.DeleteAllData();
                this.Create(projectLicenseCode);
                result++;
            }
            return result;
        }

        public void Delete(int Id)
        {
            _localLicenseDal.Delete(Id);
        }

        public void DeleteAllData()
        {
            _localLicenseDal.Truncate();
        }

        public ProjectLicenseCode GetALicense()
        {
            return _localLicenseDal.GetALicense();
        }

        public ProjectLicenseCode GetById(int Id)
        {
            return _localLicenseDal.GetById(Id);
        }

        public void Update(ProjectLicenseCode projectLicenseCode)
        {
            _localLicenseDal.Update(projectLicenseCode);
        }
    }
}
