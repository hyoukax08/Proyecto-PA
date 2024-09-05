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
                .ForMember(t => t.Actionlist, o => o.MapFrom(s => s.ActionsList.Select(a => new GrpcProtos.ControlAction()
                {
                    ActionName = a.ActionName,
                    Amount = a.Amount,
                    Measureunit = a.MeasurementUnit,
                    
                }).ToList()));





            CreateMap<GrpcProtos.FaseDTO,
                Domain.Entities.AccionElements.Fases.Fase>()
                .ForMember(t => t.Id, o => o.MapFrom(s => new Guid(s.Id)))
                .ForMember(t => t.Description, o => o.MapFrom(s => s.Description))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.Duration, o => o.MapFrom(s => s.Duration))
                .ForMember(t => t.ActionsList, o => o.MapFrom(s => s.Actionlist.Select(a => 
                new Domain.ValueObjects.ControlActions.ControlAction(a.ActionName, a.Amount, a.Measureunit)).ToList()));



            CreateMap<GrpcProtos.CreateFaseRequest,
                Domain.Entities.AccionElements.Fases.Fase>()
                .ForMember(t => t.Description, o => o.MapFrom(s => s.Description))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.ActionsList, o => o.MapFrom(s => s.Actionlist.Select(a =>
                new Domain.ValueObjects.ControlActions.ControlAction(a.ActionName, a.Amount, a.Measureunit)).ToList()));

            CreateMap<Domain.Entities.AccionElements.Fases.Fase,
                GrpcProtos.CreateFaseRequest>()
                .ForMember(t => t.Description, o => o.MapFrom(s => s.Description))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.Actionlist, o => o.MapFrom(s => s.ActionsList.Select(a => new GrpcProtos.ControlAction()
                {
                    ActionName = a.ActionName,
                    Amount = a.Amount,
                    Measureunit = a.MeasurementUnit,

                }).ToList()));
        }
    }
}

