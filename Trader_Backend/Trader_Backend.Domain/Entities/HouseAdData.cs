using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class HouseAdData : AdData
    {
        public double Area { get; set; }
        public double LandArea { get; set; }
        public int NumberOfFloors { get; set; }
        public int FloorNumber { get; set; }
        public string BuildingAge { get; set; } = null!;
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfKitchens { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasParking { get; set; }
        public bool HasGarden { get; set; }
        public bool IsFurnished { get; set; }
    }
}
