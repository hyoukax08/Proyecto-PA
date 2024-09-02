using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Products.Queries.GetProductByID
{
    public class GetProductByIDQueryHandler : IQueryHandler<GetProductByIDQuery, Product?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIDQueryHandler(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product?> Handle(GetProductByIDQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.GetProductById(request.ID));
        }
    }
}
