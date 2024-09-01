using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.Recipe;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Recipes.Queries.GetIdOfProductToMake
{
    public record GetIdOfProductToMakeQuery(Guid ID) : IQuery<Product?>;
}
