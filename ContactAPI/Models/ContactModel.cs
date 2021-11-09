using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Models
{
    public class ContactModel
    {
        [Key]
        public int ContactId { get; set; }

        public string Name { get; set; }

        public string IdNumber { get; set; }

        public string DOB { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

    }
}
