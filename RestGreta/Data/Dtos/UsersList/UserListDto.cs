using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Dtos.UsersList
{
    public record UserListDto(string id, string UserName, string Name, string Surname, string Address);
}
