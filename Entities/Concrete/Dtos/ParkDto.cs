using Entities.Abstract;
using System;

namespace Entities.Concrete.Dtos
{
    public class ParkDto : IDto
    {
        public int Id { get; set; }
        public string ParkPlaceName { get; set; }
        public string CampaignName { get; set; }
        public string Customer { get; set; }
        public string Plate { get; set; }
        public string Employee { get; set; }
        public DateTime ParkingStartDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsState { get; set; }
    }
}