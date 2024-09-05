using AutoMapper;
using ProductionRecipes.GrpcProtos;
using ProductionRecipes.Services.Mappers;
namespace ProductionRecipes.Services.Mappers
{
    public class RecipeProfile : Profile
    {
       
        public RecipeProfile()
        {
          //private readonly IMapper _mapper;
        //OperationsList.Items.AddRange(sexecOperations.Select(m => _mapper.Map<OperationDTO>(m)));

        CreateMap<Domain.Entities.Recipe.Recipe,
                GrpcProtos.RecipeDTO>()
                .ForMember(t => t.Id, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(t => t.Creationdate, o => o.MapFrom(s => s.CreationDate.ToString()))
                .ForMember(t => t.Validationdate, o => o.MapFrom(s => s.ValidationDate.ToString()))
                .ForMember(t => t.Expertname, o => o.MapFrom(s => s.Expertname))
                .ForMember(t => t.Productid, o => o.MapFrom(s => s.ProductId.ToString()))
                .ForMember(t => t.Producttomake, o => o.MapFrom(s => new GrpcProtos.ProductDTO()
                {
                    Id = s.ProductToMake.Id.ToString(),
                    Name = s.ProductToMake.Name,
                    Companyname = s.ProductToMake.CompanyName,
                    ContainerShape = (GrpcProtos.ContainerShape) s.ProductToMake.Shape
                })) 
                .ForMember(t => t.Operationlist, o => o.MapFrom(s => s.ExecOperation ));

        CreateMap<GrpcProtos.RecipeDTO,
                Domain.Entities.Recipe.Recipe>()
                .ForMember(t => t.Id, o => o.MapFrom(s => new Guid(s.Id)))
                .ForMember(t => t.CreationDate, o => o.MapFrom(s => DateTime.Parse(s.Creationdate)))
                .ForMember(t => t.ValidationDate, o => o.MapFrom(s => DateTime.Parse(s.Validationdate)))
                .ForMember(t => t.Expertname, o => o.MapFrom(s => s.Expertname))
                .ForMember(t => t.ProductId, o => o.MapFrom(s => new Guid(s.Productid)))
                .ForMember(t => t.ProductToMake, o => o.MapFrom(s => new Domain.Entities.Products.Product(s.Producttomake.Name,new Guid(s.Producttomake.Id))
                {
                    Shape = (Domain.Types.ContainerShape)s.Producttomake.ContainerShape,
                    CompanyName = s.Producttomake.Companyname
                }))
                .ForMember(t => t.ExecOperation, o => o.MapFrom(s => s.Operationlist));
            
        }
    }
}
