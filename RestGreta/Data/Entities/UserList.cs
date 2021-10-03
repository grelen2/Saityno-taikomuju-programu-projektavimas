using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Entities
{
    public class UserList
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name{ get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        
        public DateTime CreationTimeUtc { get; set; }
    }
}
