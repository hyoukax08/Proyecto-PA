using ProductionRecipes.Domain.Types;
using ProductionRecipes.Application.Recipes.Commands.CreateRecipe;
using ProductionRecipes.Application.Recipes.Commands.DeleteRecipe;
using ProductionRecipes.Application.Recipes.Commands.UpdateRecipe;
using ProductionRecipes.Application.Recipes.Commands.ValidateRecipe;
using ProductionRecipes.Application.Recipes.Queries.GetAllRecipes;
using ProductionRecipes.Application.Recipes.Queries.GetIdOfProductToMake;
using ProductionRecipes.Application.Recipes.Queries.GetRecipeByID;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;


namespace ProductionRecipes.Services.Services
{
    public class RecipeService: Recipe.RecipeBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RecipeService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public override Task<RecipeDTO> CreateRecipe(CreateRecipeRequest request, ServerCallContext context)
        {
            var command = new CreateRecipeCommand(
                //convirtiendo de tipo "repeated " a lista de fases 
               // _mapper.Map<Domain.Entities.Recipe.Recipe>(request).ProductToMake,
                _mapper.Map<Domain.Entities.Products.Product>(request.Producttomake),
                _mapper.Map<Domain.Entities.Recipe.Recipe>(request).ExecOperation);

            var result = _mediator.Send(command).Result;
            return Task.FromResult(_mapper.Map<RecipeDTO>(result)); ;
        }
        public override Task<NullableRecipeDTO> GetRecipe(GetRequest request, ServerCallContext context)
        {
            var query = new GetRecipeByIDQuery(new Guid(request.Id));

            var result = _mediator.Send(query).Result;

            if (result is null)
                return Task.FromResult(new NullableRecipeDTO() { Null = NullValue.NullValue });
            return Task.FromResult(new NullableRecipeDTO() { Recipe = _mapper.Map<RecipeDTO>(result) });
        }
        public override Task<Recipes> GetAllRecipes(Empty request, ServerCallContext context)
        {
            var query = new GetAllRecipesQuery();

            var result = _mediator.Send(query).Result;

            // Convirtiendo de lista de recipes al mensaje de lista de DTOs de recipes.
            var RecipesDTOs = new Recipes();
            RecipesDTOs.Items.AddRange(result.Select(m => _mapper.Map<RecipeDTO>(m)));

            return Task.FromResult(RecipesDTOs);
        }
        public override Task<Empty> UpdateRecipe(RecipeDTO request, ServerCallContext context)
        {
            var command = new UpdateRecipeCommand(_mapper.Map<Domain.Entities.Recipe.Recipe>(request));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteRecipe(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteRecipeCommand(new Guid(request.Id));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
        public override Task<NullableProductID> GetIdOfProductToMake(GetRequest request, ServerCallContext context)
        {
            var query = new GetIdOfProductToMakeQuery(new Guid(request.Id));

            var result = _mediator.Send(query).Result;

            if (result is null)
                return Task.FromResult(new NullableProductID() { Null = NullValue.NullValue });
            return Task.FromResult(new NullableProductID() { Producttomakeid = result.ToString() });
        }
        public override Task<Empty> ValidateRecipe(ValidateRecipeRequest request, ServerCallContext context)
        {
            var command = new ValidateRecipeCommand(
                _mapper.Map<Domain.Entities.Recipe.Recipe>(request.Recipe),
                request.Expert,
                DateTime.Parse(request.Validationdate)
                );

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
    }
}
