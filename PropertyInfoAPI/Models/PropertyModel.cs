using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyInfoAPI.Models
{
    public class PropertyModel
    {
        [Key]
        public int PropertyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Type { get; set; }

        public string ImageUrl { get; set; }
    }
}
