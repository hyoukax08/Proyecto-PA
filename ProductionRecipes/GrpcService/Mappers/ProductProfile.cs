using AutoMapper;
using ProductionRecipes.GrpcProtos;

namespace ProductionRecipes.Services.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {

            CreateMap<Domain.Entities.Products.Product,
                GrpcProtos.ProductDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(t => t.Companyname, o => o.MapFrom(s => s.CompanyName))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.ContainerShape, o => o.MapFrom(s => (GrpcProtos.ContainerShape)s.Shape));






            CreateMap<GrpcProtos.ProductDTO,
                Domain.Entities.Products.Product>()
                .ForMember(t => t.Id, o => o.MapFrom(s => new Guid(s.Id)))
                .ForMember(t => t.CompanyName, o => o.MapFrom(s => s.Companyname))
                .ForMember(t => t.Name, o => o.MapFrom(s => s.Name))
                .ForMember(t => t.Shape, o => o.MapFrom(s => (Domain.Types.ContainerShape) s.ContainerShape));


        }
    }
}


