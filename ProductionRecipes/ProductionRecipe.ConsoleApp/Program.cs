using ProductionRecipes.DataAccess;
using ProductionRecipes.DataAccess.Contexts;
using ProductionRecipes.DataAccess.FluentConfigurations;
using ProductionRecipes.DataAccess.FluentConfigurations.Products;
using ProductionRecipes.DataAccess.FluentConfigurations.Recipes;
using ProductionRecipes.DataAccess.FluentConfigurations.AccionElements;
using ProductionRecipes.DataAccess.FluentConfigurations.Common;
using ProductionRecipes.Domain.Common;
using ProductionRecipes.Domain.Entities.Products;
using ProductionRecipes.Domain.Entities.Recipe;
using ProductionRecipes.Domain.Entities.AccionElements;
using ProductionRecipes.Domain.Entities.AccionElements.Fases;
using ProductionRecipes.Domain.Entities.AccionElements.Operations;
using ProductionRecipes.Domain.Types;
using ProductionRecipes.Domain.ValueObjects;
using ProductionRecipes.Domain.ValueObjects.ControlActions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;



namespace ProductionRecipes.ConsoleApp
{
    internal class program
    {
        static void Main(string[] args)
        {
            if (File.Exists("ProductionDB.sqlite"))
                File.Delete("ProductionDB.sqlite");
            //creando un contexto para interacturar con la base de datos
            ApplicationContext applicationContext = new("Data Source=ProductionRecipeDB.sqlite");
            //Verififcando la Base de Datos
            if (!applicationContext.Database.CanConnect())
            {
                //Migrando Base de Datos.Este paso genera la base de datos con las tablas configuradas en su migracion
                applicationContext.Database.Migrate();
            }
            //creando entidades para probar BD
                Product product1 = new Product("Nicosilen",Guid.NewGuid());
                Product product2 = new Product("Paracetamol", Guid.NewGuid());
                ControlAction controlAction1 = new ControlAction("abrir valvula", 50, "%");
                ControlAction controlAction2 = new ControlAction("abrir valvula", 100, "%");
                ControlAction controlAction3 = new ControlAction("abrir valvula", 0, "%");
                List<ControlAction> listca1 = new List<ControlAction>();
                listca1.Add(controlAction1);
                listca1.Add(controlAction2);
                listca1.Add(controlAction1);
                List<ControlAction> listca2 = new List<ControlAction>();
                listca2.Add(controlAction2);
                List<ControlAction> listca3 = new List<ControlAction>();
                listca3.Add(controlAction3);

                Fase fase1 = new Fase(listca1, "Pruebas", "Fase de pruebas con aberturas progresivas", Guid.NewGuid());
                Fase fase2 = new Fase(listca2, "PruebaRapida", "Fase de prueba con apertura total", Guid.NewGuid());
                Fase fase3 = new Fase(listca3, "Cierre", "cierre total", Guid.NewGuid());

                List<Fase> listfases1 = new List<Fase>();
                listfases1.Add(fase1);
                listfases1.Add(fase3);
                List<Fase> listfases2 = new List<Fase>();
                listfases2.Add(fase2);
                listfases2.Add(fase3);

                Operation operation1 = new Operation(listfases1,"Operacion de Prueba Lenta","Pruebas progresivas y cierre",Guid.NewGuid());
                Operation operation2 = new Operation(listfases2, "Operacion de Prueba Rapida", "Pruebas Rapida y cierre", Guid.NewGuid());

                List<Operation> listOperations1 = new List<Operation>();
                listOperations1.Add(operation1);
                listOperations1.Add(operation2);

                Recipe recipe1 = new Recipe(product1, listOperations1,Guid.NewGuid());
                Recipe recipe2 = new Recipe(product2, listOperations1,Guid.NewGuid());
                //almacenando entidades en BD
                applicationContext.Products.Add(product1);
                applicationContext.Products.Add(product2);
                applicationContext.AccionElements.Add(fase1);
                applicationContext.AccionElements.Add(fase2);
                applicationContext.AccionElements.Add(fase3);
                applicationContext.AccionElements.Add(operation1);
                applicationContext.AccionElements.Add(operation2);
                applicationContext.Recipes.Add(recipe1);
                applicationContext.Recipes.Add(recipe2);
                // Es necesario guardar los cambios para que se actualice la BD
                applicationContext.SaveChanges();

                //Obteniendo entidades relacionadas a una receta
                Product? productfromrecipe = applicationContext
                    .Set<Product>()
                    .FirstOrDefault(v=>v.Id==recipe1.ProductId);
                //modificando la unidad sobre la q actua una operacion
                operation1.UnityName = "unidad inicial";
                applicationContext.AccionElements.Update(operation1);
                applicationContext.SaveChanges();
                Operation? modifiedoperation = applicationContext
                    .Set<Operation>()

                    .FirstOrDefault(o=>o.Id==operation1.Id);
                Console.WriteLine($"Nueva unidad sobre la que ejecutara la operacion: {modifiedoperation.Name} => {modifiedoperation.UnityName}");

            
                //eliminando una operacion
                applicationContext.AccionElements.Remove(operation2);
                applicationContext.SaveChanges();

                Operation? deletedoperation = applicationContext
                    .Set<Operation>()
                    .FirstOrDefault(c=>c.Id==operation2.Id);
                if (deletedoperation is null)
                    Console.WriteLine("Operation successfully deleted");

                


            }
        }
    }
