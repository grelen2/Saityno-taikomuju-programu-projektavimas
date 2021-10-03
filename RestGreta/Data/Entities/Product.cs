using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set;  }
        public DateTime CreationTimeUtc { get; set; }
    }
}
