using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.DataAccess.FluentConfigurations.Common;
using ProductionRecipes.Domain.Entities.AccionElements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductionRecipes.DataAccess.FluentConfigurations.AccionElements
{
    public class AccionElementEntityTypeConfigurationBase
        : EntityTypeConfigurationBase<AccionElement>
        {
            public override void Configure(EntityTypeBuilder<AccionElement> builder)
            {
                builder.ToTable("AccionElements");
                base.Configure(builder);

            }
        }
}

