using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IQuery<IEnumerable<Product>>;
}
