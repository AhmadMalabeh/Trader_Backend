using System;
using System.Collections.Generic;
using System.Text;

namespace Trader_Backend.Domain.Entities
{
    public class CarForSellAdData : AdData
    {
        public int StructureTypeID { get; set; }
        public int NumberOfSeats { get; set; }
        public int TransmissionTypeID { get; set; }
        public int EngineCapacityID { get; set; }
        public int FuelTypeID { get; set; }
        public string ExteriorColor { get; set; } = null!;
        public string InteriorColor { get; set; } = null!;
        public int LicensingStatusID { get; set; }
        public int InsuranceTypeID { get; set; }
        public int VehicleTypeID { get; set; }
        public int ModelTypeID { get; set; }
        public int YearOfManufacture { get; set; }
        public int VehicleStatusID { get; set; }
        public int KilometersTraveled { get; set; }
    }
}
