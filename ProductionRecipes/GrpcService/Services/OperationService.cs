using ProductionRecipes.Domain.Types;
using ProductionRecipes.Application.AccionElements.Commands.CreateOperation;
using ProductionRecipes.Application.AccionElements.Commands.DeleteOperation;
using ProductionRecipes.Application.AccionElements.Commands.UpdateOperation;
using ProductionRecipes.Application.AccionElements.Queries.GetAllOperations;
using ProductionRecipes.Application.AccionElements.Queries.GetOperationByID;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;

namespace ProductionRecipes.Services.Services
{
    public class OperationService: Operation.OperationBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OperationService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;   
            _mapper = mapper;
        }

        public override Task<OperationDTO> CreateOperation(CreateOperationRequest request, ServerCallContext context)
        {
            var command = new CreateOperationCommand(
                request.Name,
                request.Description);

            var result = _mediator.Send(command).Result;
            return Task.FromResult(_mapper.Map<OperationDTO>(result)); ;
        }

        public override Task<NullableOperationDTO> GetOperation(GetRequest request, ServerCallContext context)
        {
            var query = new GetOperationByIDQuery(new Guid(request.Id));

            var result = _mediator.Send(query).Result;

            if (result is null)
                return Task.FromResult(new NullableOperationDTO() { Null = NullValue.NullValue });
            return Task.FromResult(new NullableOperationDTO() { Operation = _mapper.Map<OperationDTO>(result) });
        }
        public override Task<Operations> GetAllOperations(Empty request, ServerCallContext context)
        {
            var query = new GetAllOperationsQuery();

            var result = _mediator.Send(query).Result;

            // Convirtiendo de lista de operaciones al mensaje de lista de DTOs de operaciones.
            var OperationsDTOs = new Operations();
            OperationsDTOs.Items.AddRange(result.Select(m => _mapper.Map<OperationDTO>(m)));

            return Task.FromResult(OperationsDTOs);
        }
        public override Task<Empty> UpdateOperation(OperationDTO request, ServerCallContext context)
        {
            var command = new UpdateOperationCommand(_mapper.Map<Domain.Entities.AccionElements.Operations.Operation>(request));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteOperation(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteOperationCommand(new Guid(request.Id));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
    }
}