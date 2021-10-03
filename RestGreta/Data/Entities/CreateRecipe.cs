using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Entities
{
    public record CreateRecipe ([Required] string RecipeName, [Required] string ProductName, [Required] string Quantity, [Required] string SeesUnits, [Required] string Description);
}
