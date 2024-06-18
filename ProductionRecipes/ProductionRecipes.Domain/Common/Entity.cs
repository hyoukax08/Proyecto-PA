using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRecipes.Domain.Common
{
    public abstract class Entity
    {
        /// <summary>
        /// identificador en BD
        /// </summary>
        #region Properties
        
        public Guid Id { get; set; }

        #endregion

        /// <summary>
        /// Requerido por EntityFramework
        /// </summary>
        protected Entity()
        {

        }
        protected Entity(Guid id)
        {
            Id = id;
        }
    }

}
