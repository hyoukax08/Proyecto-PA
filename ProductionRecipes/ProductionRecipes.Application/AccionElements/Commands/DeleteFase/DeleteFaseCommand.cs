using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;

namespace ProductionRecipes.Application.AccionElements.Commands.DeleteFase
{
    public record DeleteFaseCommand(Guid Id) : ICommand;
}
