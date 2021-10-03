using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Dtos.UsersList
{
    public record UpdateUserListDto([Required] string UserName, [Required] string Name, [Required] string Surname, string Address);
}
