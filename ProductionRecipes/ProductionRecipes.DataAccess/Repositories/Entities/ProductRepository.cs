using ProductionRecipes.Contracts.Product;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.Repositories.Common;
using ProductionRecipes.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRecipes.DataAccess.Repositories.Products
{
    /// <summary>
    /// Implementación del repositorio <see cref="IProductRepository"/>.
    /// </summary>
    public class ProductRepository
        : RepositoryBase, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product? GetProductById(Guid id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}