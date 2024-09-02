using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler
        : IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.GetAllProducts());
        }

    }
}
