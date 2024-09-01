using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler
     : ICommandHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product result = new Product(
                request.Name,
                Guid.NewGuid());

            _productRepository.AddProduct(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }
    }
}
