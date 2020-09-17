using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Park : IEntity
    {
        public int Id { get; set; }
        public int ParkPlaceId { get; set; }
        public int? CampaignId { get; set; }
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ParkingStartDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}