using Entities.Abstract;
using System;

namespace Entities.Concrete.Dtos
{
    public class ParkHistoryDto : IDto
    {
        public int Id { get; set; }
        public string ParkPlaceName { get; set; }
        public string CampaignName { get; set; }
        public string Customer { get; set; }
        public string Plate { get; set; }
        public string Employee { get; set; }
        public double Price { get; set; }
        public DateTime ParkingStartDateTime { get; set; }
        public DateTime ParkedLeaveDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}