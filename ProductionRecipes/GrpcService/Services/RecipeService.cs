using ProductionRecipes.Domain.Types;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Reflection.Metadata.Ecma335;

namespace ProductionRecipes.Services.Services
{
    public class RecipeService: Recipe.RecipeBase
    {
        public override Task<RecipeDTO> CreateRecipe(CreateRecipeRequest request, ServerCallContext context)
        {
            return base.CreateRecipe(request, context);
        }
        public override Task<NullableRecipeDTO> GetRecipe(GetRequest request, ServerCallContext context)
        {
            return base.GetRecipe(request, context);
        }
        public override Task<Recipes> GetAllRecipes(Empty request, ServerCallContext context)
        {
            return base.GetAllRecipes(request, context);
        }
        public override Task<Empty> UpdateRecipe(RecipeDTO request, ServerCallContext context)
        {
            return base.UpdateRecipe(request, context);
        }
        public override Task<Empty> DeleteRecipe(DeleteRequest request, ServerCallContext context)
        {
            return base.DeleteRecipe(request, context);
        }
        public override Task<NullableProductDTO> GetIdOfProductToMake(GetRequest request, ServerCallContext context)
        {
            return base.GetIdOfProductToMake(request, context);
        }
        public override Task<Empty> ValidateRecipe(ValidateRecipeRequest request, ServerCallContext context)
        {
            return base.ValidateRecipe(request, context);
        }
    }
}
