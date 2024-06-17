using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRecipes.Domain.Abstract
{
    /// <summary>
    /// Clases base para todas las entidades
    /// </summary>
  public interface Id
  {
        #region Properties
        public string ID { get; set; }
        #endregion
  }

}

