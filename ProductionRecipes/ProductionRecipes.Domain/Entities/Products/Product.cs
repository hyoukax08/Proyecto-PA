using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Types;

namespace ProductionRecipes.Domain.Entities.Products
{
	/// <summary>
	/// Modela el producto
	/// </summary>
	public class Product
	{
       
        #region Properties
        /// <summary>
        /// Nombre del Producto
        /// </summary>
        public string Name { get; }

		/// <summary>
		/// Nombre de la empresa que lo produce
		/// </summary>
		public string CompanyName { get; }

		/// <summary>
		/// Indicador de forma de envase
		/// </summary>
		public ContainerShape Shape { get; }
        #endregion

        public Product(string name, string companyName, ContainerShape shape)
		{
			Name = name;
			CompanyName = companyName;
			Shape = shape;
		}
	}
}
