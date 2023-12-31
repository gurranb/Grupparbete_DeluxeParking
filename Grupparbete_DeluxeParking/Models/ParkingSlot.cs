﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupparbete_DeluxeParking.Models
{
    internal class ParkingSlot
    {
        public string HouseName { get; set; }
        public string SlotNumber { get; set; }
        public bool? ElectricOutlet { get; set; }
        // Om bool ej funkar ' int? '
        public int? TotalElectricSpots { get; set; }
        public int ParkingHouseId { get; set; }
        public string CityName { get; set; }
        public string AllSlots { get; set; }
        public string OccupiedSlots { get; set; }
        public string ParkingSlots { get; set; }

    }
}
