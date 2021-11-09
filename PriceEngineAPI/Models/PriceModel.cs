using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PriceEngineAPI.Models
{
    public class PriceModel
    {
        [Key]
        public int PriceId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string ChargingPrice { get; set; }

        public string HotelId { get; set; }
    }
}
