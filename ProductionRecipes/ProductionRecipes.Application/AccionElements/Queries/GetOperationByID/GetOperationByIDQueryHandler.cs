using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;

namespace ProductionRecipes.Application.AccionElements.Queries.GetOperationByID
{
    public class GetOperationByIDQueryHandler : IQueryHandler<GetOperationByIDQuery, Operation?>
    {
        private readonly IAccionElementRepository _accionElementRepository;

        public GetOperationByIDQueryHandler(
            IAccionElementRepository accionElementRepository)
        {
            _accionElementRepository = accionElementRepository;
        }

        public Task<Operation?> Handle(GetOperationByIDQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_accionElementRepository.GetAccionElementById<Operation>(request.ID));
        }
    }
}

