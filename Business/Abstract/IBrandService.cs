using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBrandService
    {
        List<Brand> List();
        List<Brand> ListByNonDeleted();

        int Create(Brand brand);
        void Delete(int Id);
        void Update(Brand brnad);
        void ChangeIsDeletedByBrandId(int Id);
        void ChangeIsNonDeletedByBrandId(int Id);
    }
}
