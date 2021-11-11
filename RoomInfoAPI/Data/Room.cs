using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Data
{
    public class Room
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
