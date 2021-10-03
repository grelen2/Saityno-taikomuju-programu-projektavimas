﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Dtos.Comments
{
    public record CreateCommentDto([Required] string UserName, [Required] string CommentText);

}
