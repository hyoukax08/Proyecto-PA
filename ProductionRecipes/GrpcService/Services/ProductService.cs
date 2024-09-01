using ProductionRecipes.Domain.Types;
using ProductionRecipes.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Reflection.Metadata.Ecma335;

namespace ProductionRecipes.Services.Services
{
    public class ProductService:Product.ProductBase
    {
        public override Task<ProductDTO> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            return base.CreateProduct(request, context);
        }
        public override Task<NullableProductDTO> GetProduct(GetRequest request, ServerCallContext context)
        {
            return base.GetProduct(request, context);
        }
        public override Task<Products> GetAllProducts(Empty request, ServerCallContext context)
        {
            return base.GetAllProducts(request, context);
        }
        public override Task<Empty> UpdateProduct(ProductDTO request, ServerCallContext context)
        {
            return base.UpdateProduct(request, context);
        }
        public override Task<Empty> DeleteProduct(DeleteRequest request, ServerCallContext context)
        {
            return base.DeleteProduct(request, context);
        }
    }
}
