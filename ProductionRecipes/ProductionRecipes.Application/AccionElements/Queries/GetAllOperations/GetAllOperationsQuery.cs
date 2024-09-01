using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;

namespace ProductionRecipes.Application.AccionElements.Queries.GetAllOperations
{
    public record GetAllOperationsQuery : IQuery<IEnumerable<Operation>>;

}
