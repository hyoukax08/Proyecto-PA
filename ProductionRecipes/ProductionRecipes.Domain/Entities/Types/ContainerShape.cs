using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRecipes.Domain.Entities.Types
{
    /// <summary>
    /// Tipos de envase
    /// </summary>
    public enum ContainerShape
    {
        /// <summary>
        /// Forma de Botella
        /// </summary>
        Botella,
        /// <summary>
        /// Forma de Caja
        /// </summary>
        Caja,
        /// <summary>
        /// Forma de ampulas
        /// </summary>
        Ampulas,
        /// <summary>
        /// Forma de Blister
        /// </summary>
        Blister,
        /// <summary>
        /// En bolsa
        /// </summary>
        Bolsa
    }
}