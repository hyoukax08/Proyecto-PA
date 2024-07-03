using ProductionRecipes.Contracts;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.Repositories.Common;
using ProductionRecipes.Domain.Entities.AccionElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRecipes.DataAccess.Repositories.AccionElements
{
    /// <summary>
    /// Implementación del repositorio <see cref="IAccionElementRepository"/>.
    /// </summary>
    public class AccionElementRepository
        : RepositoryBase, IAccionElementRepository
    {
        public AccionElementRepository(ApplicationContext context)
            : base(context) { }

        public void AddAccionElement(AccionElement accionelement)
        {
            _context.AccionElements.Add(accionelement);
        }

        public void DeleteAccionElement(AccionElement accionelement)
        {
            _context.AccionElements.Remove(accionelement);
        }

        public IEnumerable<T> GetAllAccionElements<T>() where T : AccionElement
        {
            return _context.Set<T>().ToList();
        }

        public T? GetAccionElementById<T>(Guid id) where T : AccionElement
        {
            return _context.Set<T>().FirstOrDefault(i => i.Id == id);
        }

        public void UpdateAccionElement(AccionElement accionelement)
        {
            _context.AccionElements.Update(accionelement);
        }
    }
}