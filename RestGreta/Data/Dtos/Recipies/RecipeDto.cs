using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Dtos.Recipies
{
    public record RecipeDto(int Id,  string RecipeName,  string ProductName, string Quantity,  string SeesUnits,  string Description);
}
