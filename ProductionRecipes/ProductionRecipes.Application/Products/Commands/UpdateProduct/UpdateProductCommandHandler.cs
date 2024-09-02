using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.Products;

namespace ProductionRecipes.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler
   : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _productRepository.UpdateProduct(request.Product);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
