using AutoMapper;
using ProductionRecipes.GrpcProtos;

namespace ProductionRecipes.Services.Mappers
{
    public class OperationProfile : Profile
    {
        public OperationProfile()
        {

            CreateMap<Domain.Entities.AccionElements.Operations.Operation,
                GrpcProtos.OperationDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(t => t.Description, o => o.MapFrom(s => s.Description))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))

                .ForMember(t => t.Unityname, o => o.MapFrom(s => s.UnityName))
                .ForMember(t => t.Faselist, o => o.MapFrom(s => s.ExecFases.Select(a => new GrpcProtos.FaseDTO()
                {
                    
                    Description = a.Description,
                    Duration = a.Duration,
                    Name = a.Name,
                    Id = a.Id.ToString(),
                    
                }).ToList()));
                



            CreateMap<GrpcProtos.OperationDTO,
                Domain.Entities.AccionElements.Operations.Operation>()
                .ForMember(t => t.Id, o => o.MapFrom(s => new Guid(s.Id)))
                .ForMember(t => t.Description, o => o.MapFrom(s => s.Description))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.UnityName, o => o.MapFrom(s => s.Unityname))
                .ForMember(t => t.ExecFases, o => o.MapFrom(s => s.Faselist.Select(a => new Domain.Entities.AccionElements.Fases.Fase(
                    a.Actionlist.Select(e => new Domain.ValueObjects.ControlActions.ControlAction(e.ActionName, e.Amount, e.Measureunit)).ToList(), a.Name, a.Description,new Guid(a.Id))
                {
                    Duration = a.Duration
                }).ToList()));

        }
    }
}

