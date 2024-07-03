using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.AccionElements;

namespace ProductionRecipes.Contracts
{
    public interface IAccionElementRepository
    {
        /// <summary>
        /// Adiciona un elemento de accion al soporte de datos
        /// </summary>
        /// <param name="accionElement"></param>
        void AddAccionElement(AccionElement accionElement);

        /// <summary>
        /// Obtiene un elemento de accion del soporte de datos por su id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T? GetAccionElementById<T>(Guid id) where T : AccionElement;

        /// <summary>
        /// Obtiene todos los elementos de accion del soporte de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAllAccionElements<T>() where T : AccionElement;

        /// <summary>
        /// Actualiza el valor de un elemento de accion
        /// </summary>
        /// <param name="accionElement"></param>
        void UpdateAccionElement(AccionElement accionElement);

        /// <summary>
        /// Elimina un elemento de accion
        /// </summary>
        /// <param name="accionElement"></param>
        void DeleteAccionElement(AccionElement accionElement);
    }
}
