using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Common;

namespace ProductionRecipes.Domain.Entities.AccionElements
{
    public class AccionElement : ValueObject
    {
        #region Properties
        /// <summary>
        /// Nombre de la Operacion
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion de la operacion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Genera la clase <see langword="base"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public AccionElement(string name, string description)
        {
            Name = name;
            Description = description;
        }
        #endregion
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;

        }
    }
}
