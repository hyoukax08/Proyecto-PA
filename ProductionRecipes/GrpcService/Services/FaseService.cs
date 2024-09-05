using ProductionRecipes.Domain.Types;
using ProductionRecipes.Application.AccionElements.Commands.CreateFase;
using ProductionRecipes.Application.AccionElements.Commands.UpdateFase;
using ProductionRecipes.Application.AccionElements.Commands.DeleteFase;
using ProductionRecipes.Application.AccionElements.Queries.GetAllFases;
using ProductionRecipes.Application.AccionElements.Queries.GetFaseByID;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;

namespace ProductionRecipes.Services.Services
{
    public class FaseService : Fase.FaseBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FaseService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override Task<FaseDTO> CreateFase(CreateFaseRequest request, ServerCallContext context)
        {
            var command = new CreateFaseCommand(
                //convirtiendo de tipo "repeated controlaction" a lista de controlactions 
                _mapper.Map<Domain.Entities.AccionElements.Fases.Fase>(request).ActionsList,
                request.Name,
                request.Description);

            var result = _mediator.Send(command).Result;
            return Task.FromResult(_mapper.Map<FaseDTO>(result)); ;
        }

        public override Task<NullableFaseDTO> GetFase(GetRequest request, ServerCallContext context)
        {
            var query = new GetFaseByIDQuery(new Guid(request.Id));

            var result = _mediator.Send(query).Result;

            if (result is null)
                return Task.FromResult(new NullableFaseDTO() { Null = NullValue.NullValue });
            return Task.FromResult(new NullableFaseDTO() { Fase = _mapper.Map<FaseDTO>(result) });
        }

        public override Task<Fases> GetAllFases(Empty request, ServerCallContext context)
        {
            var query = new GetAllFasesQuery();

            var result = _mediator.Send(query).Result;

            // Convirtiendo de lista de fases al mensaje de lista de DTOs de fases.
            var FasesDTOs = new Fases();
            FasesDTOs.Items.AddRange(result.Select(m => _mapper.Map<FaseDTO>(m)));

            return Task.FromResult(FasesDTOs);
        }

        public override Task<Empty> UpdateFase(FaseDTO request, ServerCallContext context)
        {
            var command = new UpdateFaseCommand(_mapper.Map<Domain.Entities.AccionElements.Fases.Fase>(request));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteFase(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteFaseCommand(new Guid(request.Id));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
    }
}