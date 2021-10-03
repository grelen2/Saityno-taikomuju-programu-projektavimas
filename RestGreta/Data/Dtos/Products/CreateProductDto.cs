using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestGreta.Data.Dtos.Products
{
    public record CreateProductDto([Required] string Name);
}
