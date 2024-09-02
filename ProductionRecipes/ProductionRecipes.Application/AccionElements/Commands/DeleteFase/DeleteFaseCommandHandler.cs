using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Application.Abstract;
using ProductionRecipes.Contracts;

namespace ProductionRecipes.Application.AccionElements.Commands.DeleteFase
{
    public class DeleteFaseCommandHandler
        : ICommandHandler<DeleteFaseCommand>
    {
        private readonly IAccionElementRepository _accionElementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFaseCommandHandler(
            IAccionElementRepository accionElementRepository,
            IUnitOfWork unitOfWork)
        {
            _accionElementRepository = accionElementRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(DeleteFaseCommand request, CancellationToken cancellationToken)
        {
            var faseToDelete = _accionElementRepository.GetAccionElementById<Fase>(request.Id);
            if (faseToDelete is null)
                return Task.CompletedTask;
            _accionElementRepository.DeleteAccionElement(faseToDelete);
            _unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
