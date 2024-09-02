using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;

namespace ProductionRecipes.Application.AccionElements.Queries.GetAllOperations
{
    public class GetAllOperationsQueryHandler
        : IQueryHandler<GetAllOperationsQuery, IEnumerable<Operation>>
    {
        private readonly IAccionElementRepository _accionElementRepository;

        public GetAllOperationsQueryHandler(
            IAccionElementRepository accionElementRepository)
        {
            _accionElementRepository = accionElementRepository;
        }

        public Task<IEnumerable<Operation>> Handle(GetAllOperationsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_accionElementRepository.GetAllAccionElements<Operation>());
        }

    }
}
