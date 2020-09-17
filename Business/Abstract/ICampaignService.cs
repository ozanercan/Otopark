using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Business.Abstract
{
    public interface ICampaignService
    {
        List<Campaign> List();
        Campaign Find(int Id);
    }
}