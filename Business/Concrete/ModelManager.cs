using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ModelManager : IModelService
    {
       private IModelDal _modelDal;
       private IBrandService _brandService;

        public ModelManager(IModelDal modelDal, IBrandService brandService)
        {
            _modelDal = modelDal;
            _brandService = brandService;
        }

        public int Create(Model parameter)
        {
            return _modelDal.Create(parameter);
        }

        public void Delete(int Id)
        {
            _modelDal.Delete(Id);
        }

        public Model Find(int Id)
        {
            return _modelDal.Find(Id);
        }

        public List<Model> List()
        {
            return _modelDal.List();
        }

        public List<Model> ListByBrandId(int brandId)
        {
            return _modelDal.ListByBrandId(brandId);
        }

        public List<Model> ListByNonDeleted()
        {
            return _modelDal.ListByNonDeleted();
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _modelDal.ChangeIsDeleteField(Id, isDeleted);
        }

        public void Update(Model parameter)
        {
            _modelDal.Update(parameter);
        }

        public List<ModelDto> ListDto()
        {
            var models = _modelDal.List();
            var brands = _brandService.ListByNonDeleted();

            var dtoModels = new List<ModelDto>();

            foreach (var model in models)
            {
                foreach (var brand in brands)
                {
                    if (model.BrandId == brand.Id)
                    {
                        dtoModels.Add(new ModelDto()
                        {
                            Id = model.Id,
                            Model = model.Name,
                            Brand = brand.Name,
                            IsDeleted = model.IsDeleted
                        });
                    }
                }
            }
            return dtoModels;
        }

        public List<Model> ListByBrandIdAndNonDeleted(int brandId)
        {
            return _modelDal.ListByBrandIdAndNonDeleted(brandId);
        }

        public List<ModelDto> ListDtoByNonDeleted()
        {
            var models = _modelDal.ListByNonDeleted();
            var brands = _brandService.List();

            var dtoModels = new List<ModelDto>();

            foreach (var model in models)
            {
                foreach (var brand in brands)
                {
                    if (model.BrandId == brand.Id)
                    {
                        dtoModels.Add(new ModelDto()
                        {
                            Id = model.Id,
                            Model = model.Name,
                            Brand = brand.Name,
                            IsDeleted = model.IsDeleted
                        });
                    }
                }
            }
            return dtoModels;
        }
    }
}
