using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler
    : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = _productRepository.GetProductById(request.Id);
            if (productToDelete is null)
                return Task.CompletedTask;
            _productRepository.DeleteProduct(productToDelete);
            _unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
