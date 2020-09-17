using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class ParkHistory : Park, IEntity
    {
        public double Price { get; set; }
        public DateTime ParkedLeaveDateTime { get; set; }
    }
}