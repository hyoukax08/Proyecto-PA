using ProductionRecipes.Domain.Types;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Reflection.Metadata.Ecma335;

namespace ProductionRecipes.Services.Services
{
    public class FaseService : Fase.FaseBase
    {
        public override Task<FaseDTO> CreateFase(CreateFaseRequest request, ServerCallContext context)
        {
            return base.CreateFase(request, context);
        }
        public override Task<NullableFaseDTO> GetFase(GetRequest request, ServerCallContext context)
        {
            return base.GetFase(request, context);
        }
        public override Task<Fases> GetAllFases(Empty request, ServerCallContext context)
        {
            return base.GetAllFases(request, context);
        }
        public override Task<Empty> UpdateFase(FaseDTO request, ServerCallContext context)
        {
            return base.UpdateFase(request, context);
        }
        public override Task<Empty> DeleteFase(DeleteRequest request, ServerCallContext context)
        {
            return base.DeleteFase(request, context);
        }
    }
}