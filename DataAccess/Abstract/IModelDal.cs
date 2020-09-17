using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IModelDal : IBasicMethods<Model>, IDeleteEntity<Model>
    {
        /// <summary>
        /// Modelleri Marka Id'sine göre listeler.
        /// </summary>
        List<Model> ListByBrandId(int brandId);

        /// <summary>
        /// Modelleri Marka Id'sine göre listeler ancak IsDeleted alanı 0 koşulu aranır.
        /// </summary>
        List<Model> ListByBrandIdAndNonDeleted(int brandId);
    }
}
