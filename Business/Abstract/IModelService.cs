using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IModelService
    {
        List<ModelDto> ListDto();
        List<ModelDto> ListDtoByNonDeleted();
        List<Model> List();

        int Create(Model model);
        void Update(Model model);
        void Delete(int Id);
        List<Model> ListByBrandId(int brandId);
        List<Model> ListByBrandIdAndNonDeleted(int brandId);
        List<Model> ListByNonDeleted();
    }
}
