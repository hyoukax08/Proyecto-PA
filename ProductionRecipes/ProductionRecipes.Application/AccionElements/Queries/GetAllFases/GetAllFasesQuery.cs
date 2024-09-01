using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;

namespace ProductionRecipes.Application.AccionElements.Queries.GetAllFases
{
    public record GetAllFasesQuery : IQuery<IEnumerable<Fase>>;

}
