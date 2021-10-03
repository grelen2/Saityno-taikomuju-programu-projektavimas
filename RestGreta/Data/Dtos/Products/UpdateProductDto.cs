using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestGreta.Data.Dtos.Products
{
    public record UpdateProductDto([Required] string Name);
}
