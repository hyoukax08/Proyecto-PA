using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Types;
using ProductionRecipes.Domain.Common;

namespace ProductionRecipes.Domain.Entities.Products
{
	/// <summary>
	/// Modela el producto
	/// </summary>
	public class Product : Entity
	{
       
        #region Properties
        /// <summary>
        /// Nombre del Producto
        /// </summary>
        public string Name { get; set; }

		/// <summary>
		/// Nombre de la empresa que lo produce
		/// </summary>
		public string? CompanyName { get; set; }

		/// <summary>
		/// Indicador de forma de envase
		/// </summary>
		public ContainerShape Shape { get; set; }
        #endregion
		/// <summary>
		/// modela el producto a crear
		/// </summary>
		/// <param name="name"></param>
        public Product(string name)
		{
			Name = name;
		}
	}
}
