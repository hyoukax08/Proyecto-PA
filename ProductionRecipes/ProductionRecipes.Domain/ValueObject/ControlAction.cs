using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Common;

namespace ProductionRecipes.Domain.ValueObjects.ControlActions
{
    /// <summary>
    /// modela la accion de control
    /// </summary>
    public class ControlAction : ValueObject
    { 
        #region Properties
        /// <summary>
        /// Accion a ejecutar.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Valor de la accion.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Unidad de medida o porcentaje de la accion.
        /// </summary>
        public string MeasurementUnit { get; set; }
        #endregion

        /// <summary>
        /// Requerido por EntityFramework para migraciones
        /// </summary>
        protected ControlAction() { }

        /// <summary>
        /// Inicializa la accion de control
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="amount"></param>
        /// <param name="measurementUnit"></param>
        public ControlAction(string actionName, int amount, string measurementUnit)
        {
            ActionName = actionName;
            Amount = amount;
            MeasurementUnit = measurementUnit;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ActionName;
            yield return Amount;
            yield return MeasurementUnit;
        }

    }
}