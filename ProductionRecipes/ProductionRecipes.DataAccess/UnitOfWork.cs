using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.Contracts;
using ProductionRecipes.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ProductionRecipes.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            if (!context.Database.CanConnect())
                context.Database.Migrate();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
