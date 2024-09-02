using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;

namespace ProductionRecipes.Application.AccionElements.Commands.DeleteOperation
{
    public record DeleteOperationCommand(Guid Id) : ICommand;
}
