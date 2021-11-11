using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Models
{
    public class PriceModel
    {
        public int PriceId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string ChargingPrice { get; set; }

        public string HotelId { get; set; }
    }
}
