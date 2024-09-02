using AutoMapper;
using ProductionRecipes.GrpcProtos;

namespace ProductionRecipes.Services.Mappers
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Domain.Entities.Recipe.Recipe,
                GrpcProtos.RecipeDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id.ToString()))
                //   .ForMember(t => t.Creationdate, o => o.MapFrom(s => ))
                //   .ForMember(t => t.Validationdate, o => o.MapFrom(s => ))
                .ForMember(t => t.Expertname, o => o.MapFrom(s => s.Expertname))
                .ForMember(t => t.Productid, o => o.MapFrom(s => s.ProductId))

                // .ForMember(t => t.Producttomake, o => o.MapFrom(s => s.ProductId)) reuitilizar mapeo de producto??
                .ForMember(t => t.Operationlist, o => o.MapFrom(s => new Google.Protobuf.Collections.RepeatedField<Domain.Entities.AccionElements.Operations.Operation>()));


            CreateMap<GrpcProtos.RecipeDTO,
                Domain.Entities.Recipe.Recipe>()
                .ForMember(t => t.Id, o => o.MapFrom(s => new Guid(s.Id)))


                .ForMember(t => t.Expertname, o => o.MapFrom(s => s.Expertname))
                .ForMember(t => t.ProductId, o => o.MapFrom(s => s.Productid))

                .ForMember(t => t.ExecOperation, o => o.MapFrom(s => new List<OperationDTO>()));
        }
    }
}
