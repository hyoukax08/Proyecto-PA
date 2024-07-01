using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionRecipes.DataAccess.FluentConfigurations.Common;
using ProductionRecipes.Domain.Entities.Recipe;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;

namespace ProductionRecipes.DataAccess.FluentConfigurations.Recipes
{
    public class RecipeEntityTypeConfiguration
        : EntityTypeConfigurationBase<Recipe>
    {
        public override void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");
            base.Configure(builder);
            builder.HasMany(x => x.ExecOperation);
            builder.Ignore(x => x.ExecOperation);
            builder.HasOne(x => x.ProductToMake).WithMany().HasForeignKey(x => x.ProductId);
        }
    }
}
