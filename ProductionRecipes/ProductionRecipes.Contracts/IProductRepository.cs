using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Products;
namespace ProductionRecipes.Contracts
{
    public interface IProductRepository
    {
        /// <summary>
        /// Adiciona un producto al soporte de datos
        /// </summary>
        /// <param name="product"></param>
        void AddProduct(Product product);

        /// <summary>
        /// Obtiene un producto del soporte de datos por su id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Product? GetProductById(Guid id);

        /// <summary>
        /// Obtiene todos los productos del soporte de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts();

        /// <summary>
        /// Actualiza el valor de un producto
        /// </summary>
        /// <param name="product"></param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="product"></param>
        void DeleteProduct(Product product);
    }
}
