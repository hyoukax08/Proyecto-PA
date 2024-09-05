using AutoMapper;
using ProductionRecipes.GrpcProtos;

namespace ProductionRecipes.Services.Mappers
{
    public class FaseProfile : Profile
    {
        public FaseProfile()
        {

            CreateMap<Domain.Entities.AccionElements.Fases.Fase,
                GrpcProtos.FaseDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(t => t.Description, o => o.MapFrom(s => s.Description))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.Duration, o => o.MapFrom(s => s.Duration))
                .ForMember(t => t.Actionlist, o => o.MapFrom(s => s.ActionsList));





            CreateMap<GrpcProtos.FaseDTO,
                Domain.Entities.AccionElements.Fases.Fase>()
                .ForMember(t => t.Id, o => o.MapFrom(s => new Guid(s.Id)))
                .ForMember(t => t.Description, o => o.MapFrom(s => s.Description))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.Duration, o => o.MapFrom(s => s.Duration))
                .ForMember(t => t.ActionsList, o => o.MapFrom(s => s.Actionlist));

        }
    }
}

