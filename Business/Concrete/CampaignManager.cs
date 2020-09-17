using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CampaignManager : ICampaignService
    {
        private ICampaignDal _campaignDal;

        public CampaignManager(ICampaignDal campaignDal)
        {
            _campaignDal = campaignDal;
        }
        public List<Campaign> ListByNonDeleted()
        {
            return _campaignDal.ListByNonDeleted();
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _campaignDal.ChangeIsDeleteField(Id, isDeleted);
        }

        public List<Campaign> List()
        {
            return _campaignDal.List();
        }

        public Campaign Find(int Id)
        {
            return _campaignDal.Find(Id);
        }
    }
}