using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.ValueObjects.ControlActions;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.AccionElements.Commands.CreateFase
{
    public class CreateFaseCommandHandler
        : ICommandHandler<CreateFaseCommand, Fase>
    {
        private readonly IAccionElementRepository _accionElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFaseCommandHandler(
            IAccionElementRepository accionElementRepository,
            IUnitOfWork unitOfWork)
        {
            _accionElementRepository = accionElementRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Fase> Handle(CreateFaseCommand request, CancellationToken cancellationToken)
        {
            Fase result = new Fase(
                request.ActionsList,
                request.Name,
                request.Description,
                Guid.NewGuid());

            _accionElementRepository.AddAccionElement(result);
            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }
    }
}
