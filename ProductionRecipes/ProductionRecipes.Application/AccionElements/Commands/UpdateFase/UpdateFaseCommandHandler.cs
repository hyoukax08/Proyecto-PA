using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.AccionElements.Commands.UpdateFase
{
    public class UpdateFaseCommandHandler
        : ICommandHandler<UpdateFaseCommand>
    {
        private readonly IAccionElementRepository _accionElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFaseCommandHandler(
            IAccionElementRepository accionElementRepository,
            IUnitOfWork unitOfWork)
        {
            _accionElementRepository = accionElementRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(UpdateFaseCommand request, CancellationToken cancellationToken)
        {
            _accionElementRepository.UpdateAccionElement(request.Fase);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}