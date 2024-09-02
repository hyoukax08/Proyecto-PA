using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;

namespace ProductionRecipes.Application.AccionElements.Queries.GetAllFases
{
    public class GetAllFasesQueryHandler
        : IQueryHandler<GetAllFasesQuery, IEnumerable<Fase>>
    {
        private readonly IAccionElementRepository _accionElementRepository;

        public GetAllFasesQueryHandler(
            IAccionElementRepository accionElementRepository)
        {
            _accionElementRepository = accionElementRepository;
        }

        public Task<IEnumerable<Fase>> Handle(GetAllFasesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_accionElementRepository.GetAllAccionElements<Fase>());
        }
    }
}
