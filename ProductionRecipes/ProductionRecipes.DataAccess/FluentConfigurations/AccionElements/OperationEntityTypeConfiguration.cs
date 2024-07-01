﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.DataAccess.FluentConfigurations.Common;
using ProductionRecipes.Domain.Entities.AccionElements;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ProductionRecipes.DataAccess.FluentConfigurations.AccionElements
{
    public class OperationEntityTypeConfiguration
        : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.ToTable("OPerations");
            builder.HasBaseType(typeof(AccionElement));
            builder.Ignore(x=>x.ExecFases);
            builder.HasMany(x=>x.ExecFases);
        }
    }
}
