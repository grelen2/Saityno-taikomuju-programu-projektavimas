using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Dtos.Comments
{
    public record CommentDto(int Id, string UserName, string CommentText);
}
