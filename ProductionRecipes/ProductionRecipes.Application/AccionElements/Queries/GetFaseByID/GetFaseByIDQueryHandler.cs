using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.AccionElements.Queries.GetFaseByID
{
    public class GetFaseByIDQueryHandler : IQueryHandler<GetFaseByIDQuery, Fase?>
    {
        private readonly IAccionElementRepository _accionElementRepository;

        public GetFaseByIDQueryHandler(
            IAccionElementRepository accionElementRepository)
        {
            _accionElementRepository = accionElementRepository;
        }

        public Task<Fase?> Handle(GetFaseByIDQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_accionElementRepository.GetAccionElementById<Fase>(request.ID));
        }
    }
}
