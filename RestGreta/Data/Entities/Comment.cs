using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CommentText { get; set; }
        public DateTime CreationTimeUtc { get; set; }
    }
}
