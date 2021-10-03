using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Dtos.Recipies
{
    public record CreateRecipeDto([Required] string RecipeName, [Required] string ProductName, [Required] string Quantity, [Required] string SeesUnits, [Required] string Description);

}
