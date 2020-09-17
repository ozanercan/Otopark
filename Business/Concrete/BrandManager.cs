using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        public int Create(Brand parameter)
        {
            return _brandDal.Create(parameter);
        }

        public void Delete(int Id)
        {
            _brandDal.Delete(Id);
        }

        public Brand Find(int Id)
        {
            return _brandDal.Find(Id);
        }

        public List<Brand> List()
        {
            return _brandDal.List();
        }

        public List<Brand> ListByNonDeleted()
        {
            return _brandDal.ListByNonDeleted();
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _brandDal.ChangeIsDeleteField(Id, isDeleted);
        }

        public void Update(Brand parameter)
        {
            _brandDal.Update(parameter);
        }

        public void ChangeIsDeletedByBrandId(int Id)
        {
            _brandDal.ChangeIsDeleteField(Id, true);
        }

        public void ChangeIsNonDeletedByBrandId(int Id)
        {
            _brandDal.ChangeIsDeleteField(Id, false);
        }
    }
}
