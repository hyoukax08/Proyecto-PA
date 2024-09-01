using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProductionRecipes.Application.Abstract
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
