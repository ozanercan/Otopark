using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICampaignDal : IBasicMethods<Campaign>, IDeleteEntity<Campaign>
    {
    }
}