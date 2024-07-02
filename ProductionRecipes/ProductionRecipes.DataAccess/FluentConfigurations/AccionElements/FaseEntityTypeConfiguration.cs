using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.DataAccess.FluentConfigurations.Common;
using ProductionRecipes.Domain.Entities.AccionElements;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductionRecipes.DataAccess.FluentConfigurations.AccionElements
{
    public class FaseEntityTypeConfiguration
        : IEntityTypeConfiguration<Fase>
    {
        public void Configure(EntityTypeBuilder<Fase> builder)
        {
            builder.ToTable("Fases");
            builder.HasBaseType(typeof(AccionElement));
            builder.OwnsMany(x => x.ActionsList);
            builder.Ignore(x=>x.ActionsList);
        }
    }
}
