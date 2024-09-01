using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Application.Abstract;

namespace ProductionRecipes.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name): ICommand<Product>;
    
}
