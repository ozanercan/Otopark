using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Campaign : IEntity
    {
        public int Id { get; set; }
        public int ParkPlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double PricePerMin { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime FinishingDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}