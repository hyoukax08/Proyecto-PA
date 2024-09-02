using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Recipe;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Recipes.Queries.GetIdOfProductToMake
{
    public class GetIdOfproductToMakeQueryHandler : IQueryHandler<GetIdOfProductToMakeQuery, Guid?>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetIdOfproductToMakeQueryHandler(
            IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Task<Guid?> Handle(GetIdOfProductToMakeQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_recipeRepository.GetIdOfProductToMake(request.ID));
        }
    }
}
