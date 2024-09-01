using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Application.Abstract;

namespace ProductionRecipes.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand;
}
