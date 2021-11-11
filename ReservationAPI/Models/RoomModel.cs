using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Models
{
    public class RoomModel
    {
        [Key]
        public int RoomId { get; set; }

        public int RoomNumber { get; set; }

        public string RoomType { get; set; }

        public string RoomStatus { get; set; }

        public int PropertyId { get; set; }

        public int PriceId { get; set; }

        public string FeaturesIds { get; set; }
    }
}
