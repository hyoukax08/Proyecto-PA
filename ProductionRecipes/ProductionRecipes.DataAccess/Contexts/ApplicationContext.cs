using ProductionRecipes.Domain.Entities.Recipe;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Domain.Entities.AccionElements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;
using System.Drawing;
using ProductionRecipes.DataAccess.FluentConfigurations;
using Microsoft.Data.Sqlite;
using ProductionRecipes.DataAccess.FluentConfigurations.AccionElements;
using ProductionRecipes.DataAccess.FluentConfigurations.Products;
using ProductionRecipes.DataAccess.FluentConfigurations.Recipes;


namespace ProductionRecipes.DataAccess.Contexts
{
    public class ApplicationContext : DbContext
    {
        //Región destinada a la declaración de las tablas de las entidades base
        #region Tables 
        /// <summary>
        /// Tabla de Elementos de Accion
        /// </summary>
        public DbSet<AccionElement> AccionElements { get; set; }
        /// <summary>
        /// Tabla de órdenes de compra.
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Tabla de vehículos.
        /// </summary>
        public DbSet<Recipe> Recipes { get; set; }

        #endregion

        /// <summary>
        /// Requerido por EntityFrameworkCore para migraciones.
        /// </summary>
        public ApplicationContext()
        {
        }
        /// <summary>
        /// Inicializa un objeto <see cref="ApplicationContext"/>.
        /// </summary>
        /// <param name="connectionString">
        /// Cadena de conexión.
        /// </param>
        public ApplicationContext(string connectionString)
            : base(GetOptions(connectionString))
        {
        }

        /// <summary>
        /// Inicializa un objeto <see cref="ApplicationContext"/>.
        /// </summary>
        /// <param name="options">
        /// Opciones del contexto.
        /// </param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccionElementEntityTypeConfigurationBase());
            modelBuilder.ApplyConfiguration(new OperationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FaseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeEntityTypeConfiguration());
        }
        #region Helpers

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqliteDbContextOptionsBuilderExtensions.UseSqlite(new DbContextOptionsBuilder(), connectionString).Options;
        }

        #endregion

    }
}
