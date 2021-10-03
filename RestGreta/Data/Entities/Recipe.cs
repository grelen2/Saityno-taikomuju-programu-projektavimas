using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string ProductName { get; set; }
        public string  Quantity { get; set; }
        public string SeesUnits { get; set; }
        public string Description { get; set; }
        public DateTime CreationTimeUtc { get; set; }
    }
}
