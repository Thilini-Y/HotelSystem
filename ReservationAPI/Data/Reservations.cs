using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Data
{
    public class Reservations
    {
        [Key]
        public int ReservationId { get; set; }

        public int ReservationNumber { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName ="Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public int ContactId { get; set; }

        public int PropertyId { get; set; }

        public int RoomId { get; set; }
    }
}
