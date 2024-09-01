using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Application.Abstract;

namespace ProductionRecipes.Application.AccionElements.Commands.UpdateFase
{
    public record class UpdateFaseCommand(Fase Fase) : ICommand;
}