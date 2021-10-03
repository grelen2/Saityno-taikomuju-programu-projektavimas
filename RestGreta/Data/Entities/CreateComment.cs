using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Entities
{
    public record CreateComment([Required] string UserName, [Required] string CommentText);

}
