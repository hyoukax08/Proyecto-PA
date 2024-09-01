using ProductionRecipes.Domain.Types;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Reflection.Metadata.Ecma335;

namespace ProductionRecipes.Services.Services
{
    public class OperationService: Operation.OperationBase
    {
        public override Task<OperationDTO> CreateOperation(CreateOperationRequest request, ServerCallContext context)
        {
            return base.CreateOperation(request, context);
        }
        public override Task<NullableOperationDTO> GetOperation(GetRequest request, ServerCallContext context)
        {
            return base.GetOperation(request, context);
        }
        public override Task<Operations> GetAllOperations(Empty request, ServerCallContext context)
        {
            return base.GetAllOperations(request, context);
        }
        public override Task<Empty> UpdateOperation(OperationDTO request, ServerCallContext context)
        {
            return base.UpdateOperation(request, context);
        }
        public override Task<Empty> DeleteOperation(DeleteRequest request, ServerCallContext context)
        {
            return base.DeleteOperation(request, context);
        }
    }
}