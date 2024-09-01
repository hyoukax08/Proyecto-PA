using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.ValueObjects.ControlActions;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Application.Abstract;
namespace ProductionRecipes.Application.AccionElements.Commands.CreateFase
{
    public record CreateFaseCommand(List<ControlAction> ActionsList, string Name, string Description): ICommand<Fase>;
    
}
