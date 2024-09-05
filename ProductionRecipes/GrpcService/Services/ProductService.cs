using ProductionRecipes.Domain.Types;
using ProductionRecipes.Application.Products.Commands.CreateProduct;
using ProductionRecipes.Application.Products.Commands.DeleteProduct;
using ProductionRecipes.Application.Products.Commands.UpdateProduct;
using ProductionRecipes.Application.Products.Queries.GetAllProducts;
using ProductionRecipes.Application.Products.Queries.GetProductByID;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;

namespace ProductionRecipes.Services.Services
{
    public class ProductService:Product.ProductBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override Task<ProductDTO> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            var command = new CreateProductCommand(
                request.Name);

            var result = _mediator.Send(command).Result;
            return Task.FromResult(_mapper.Map<ProductDTO>(result)); ;
        }

        public override Task<NullableProductDTO> GetProduct(GetRequest request, ServerCallContext context)
        {
            var query = new GetProductByIDQuery(new Guid(request.Id));

            var result = _mediator.Send(query).Result;

            if (result is null)
                return Task.FromResult(new NullableProductDTO() { Null = NullValue.NullValue });
            return Task.FromResult(new NullableProductDTO() { Product = _mapper.Map<ProductDTO>(result) });
        }

        public override Task<Products> GetAllProducts(Empty request, ServerCallContext context)
        {
            var query = new GetAllProductsQuery();

            var result = _mediator.Send(query).Result;

            // Convirtiendo de lista de products al mensaje de lista de DTOs de productos.
            var ProductsDTOs = new Products();
            ProductsDTOs.Items.AddRange(result.Select(m => _mapper.Map<ProductDTO>(m)));

            return Task.FromResult(ProductsDTOs);
        }

        public override Task<Empty> UpdateProduct(ProductDTO request, ServerCallContext context)
        {
            var command = new UpdateProductCommand(_mapper.Map<Domain.Entities.Products.Product>(request));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
        public override Task<Empty> DeleteProduct(DeleteRequest request, ServerCallContext context)
        {
            var command = new DeleteProductCommand(new Guid(request.Id));

            _mediator.Send(command);

            return Task.FromResult(new Empty());
        }
    }
}
