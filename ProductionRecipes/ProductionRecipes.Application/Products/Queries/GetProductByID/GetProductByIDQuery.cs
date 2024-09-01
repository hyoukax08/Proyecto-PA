using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Products.Queries.GetProductByID
{
    public record GetProductByIDQuery(Guid ID) : IQuery<Product?>;
    
}
