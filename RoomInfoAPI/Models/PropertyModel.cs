using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Models
{
    public class PropertyModel
    {
        public int PropertyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Type { get; set; }

        public string ImageUrl { get; set; }
    }
}
