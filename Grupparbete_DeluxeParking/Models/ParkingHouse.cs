using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupparbete_DeluxeParking.Models
{
    internal class ParkingHouse
    {
        public int Id { get; set; }
        public string HouseName { get; set; }
        public int CityId { get; set; }
        public int TotalParkingSlots { get; set; }
    }
}
