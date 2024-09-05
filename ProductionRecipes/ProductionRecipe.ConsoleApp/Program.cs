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
using ProductionRecipes.GrpcProtos;
using Grpc.Net.Client;
using ProductionRecipes.Services.Mappers;
using ProductionRecipes.Services.Services;




namespace ProductionRecipes.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presione una tecla para conectar");
            Console.ReadKey();

            Console.WriteLine("Creating channel and client");
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://localhost:5051", new GrpcChannelOptions { HttpHandler = httpHandler });
            if (channel is null)
            {
                Console.WriteLine("Cannot connect");
                channel.Dispose();
                return;
            }
            bool turnoff = false;
            while (turnoff == false)
            {
                //Console.Clear();
                Console.WriteLine("Menu Principal");
                Console.WriteLine("Presione 1 para modificar productos, 2 para recetas y sus derivados, 3 Salir");

                var keypressed = Console.ReadLine();

                switch (keypressed)
                {
                    case "1"://modificando productos
                        {
                            var client = new ProductionRecipes.GrpcProtos.Product.ProductClient(channel);
                            Console.Clear();
                            Console.WriteLine("1: Crear un Producto  2: Modificar un Producto");
                            Console.WriteLine("3: Borrar un Producto 4: Obtener Informacion de un producto");
                            Console.WriteLine("5: Obtener el total de Productos");

                            var producttask = Console.ReadLine();
                            switch (producttask)
                            {
                                case "1":
                                    {
                                        CrearProducto(client);
                                        break;

                                    }//crear producto
                                case "2":
                                    {
                                        ModificarProducto(client);
                                        break;

                                    }//Modificar el pproducto
                                case "3":
                                    {
                                        EliminarProducto(client);
                                        break;

                                    }//Borrar producto
                                case "4":
                                    {
                                        ObtenerProducto(client);
                                        break;
                                    }//Obtener informacion de un producto
                                case "5":
                                    {
                                        ObtenerTodosLosProductos(client);
                                        break;

                                    }//Obtener el total de productos
                                default:
                                    Console.WriteLine("Error de tecleo");
                                    return;
                            }
                            break;
                        }
                    case "2"://modificando recetas y derivados
                        {
                            Console.Clear();
                            Console.WriteLine("1: Recetas  2: Operaciones");
                            Console.WriteLine("3: Fases");


                            var select = Console.ReadLine();
                            switch (select)
                            {
                                case "1"://Trabajo con Recetas
                                    {
                                        var client = new ProductionRecipes.GrpcProtos.Recipe.RecipeClient(channel);
                                        var clientproduct = new ProductionRecipes.GrpcProtos.Product.ProductClient(channel);
                                        Console.Clear();
                                        Console.WriteLine("1: Crear una Receta  2: Modificar una Receta");
                                        Console.WriteLine("3: Borrar una Receta 4: Obtener Informacion de una Receta");
                                        Console.WriteLine("5: Obtener el total de Recetas 6: Validar una Receta");
                                        Console.WriteLine("7: Obtener producto de una receta");
                                        var recipetask = Console.ReadLine();
                                        switch (recipetask)
                                        {
                                            case "1":
                                                {
                                                    CrearReceta(client, clientproduct);
                                                    break;
                                                }//crearreceta
                                            case "2":
                                                {
                                                    ModificarReceta(client, clientproduct);
                                                    break;
                                                }//modificarreceta
                                            case "3":
                                                {
                                                    EliminarReceta(client);
                                                    break;
                                                }//eliminarreceta
                                            case "4":
                                                {
                                                    ObtenerReceta(client);
                                                    break;
                                                }//obtenerreceta
                                            case "5":
                                                {
                                                    ObtenerTodasLasRecetas(client);
                                                    break;
                                                }//obtenertodaslasrecetas
                                            case "6":
                                                {
                                                    ValidarReceta(client);
                                                    break;
                                                }//validarreceta
                                            case "7":
                                                {
                                                    ObtenerProductodeReceta(client);
                                                    break;
                                                }//obtenerproductodereceta
                                        }
                                        break;
                                    }
                                case "2"://Trabajo con Operaciones
                                    {
                                        var client = new ProductionRecipes.GrpcProtos.Operation.OperationClient(channel);
                                        var faseclient = new ProductionRecipes.GrpcProtos.Fase.FaseClient(channel);
                                        Console.Clear();
                                        Console.WriteLine("1: Crear una Operacion  2: Modificar una Operacion");
                                        Console.WriteLine("3: Borrar una Operacion 4: Obtener Informacion de una Operacion");
                                        Console.WriteLine("5: Obtener el total de Operaciones");
                                        var optask = Console.ReadLine();
                                        switch (optask)
                                        {
                                            case "1":
                                                {
                                                    CrearOperacion(client);
                                                    break;
                                                }//crearOp
                                            case "2":
                                                {
                                                    ModificarOperacion(client, faseclient);
                                                    break;
                                                }//modificarOp
                                            case "3":
                                                {
                                                    EliminarOperacion(client);
                                                    break;
                                                }//eliminarOp
                                            case "4":
                                                {
                                                    ObtenerOperacion(client);
                                                    break;
                                                }//obtenerOp
                                            case "5":
                                                {
                                                    ObtenerTodasLasOperaciones(client);
                                                    break;
                                                }//obtenertodaslasOp
                                        }
                                        break;
                                    }
                                case "3"://Trabajo con fases
                                    {
                                        var client = new ProductionRecipes.GrpcProtos.Fase.FaseClient(channel);
                                        Console.Clear();
                                        Console.WriteLine("1: Crear una Fase  2: Modificar una Fase");
                                        Console.WriteLine("3: Borrar una Fase 4: Obtener Informacion de una Fase");
                                        Console.WriteLine("5: Obtener el total de Fases");
                                        var fasetask = Console.ReadLine();
                                        switch (fasetask)
                                        {
                                            case "1":
                                                {
                                                    CrearFase(client);
                                                    break;
                                                }//crearfase
                                            case "2":
                                                {
                                                    ModificarFase(client);
                                                    break;
                                                }//modificarfase
                                            case "3":
                                                {
                                                    EliminarFase(client);
                                                    break;
                                                }//eliminarfase
                                            case "4":
                                                {
                                                    ObtenerFase(client);
                                                    break;
                                                }//obtenerfase
                                            case "5":
                                                {
                                                    ObtenerTodasLasFases(client);
                                                    break;
                                                }//obtenertodaslasfases
                                        }
                                        break;
                                    }

                                default:
                                    Console.WriteLine("Numero Equivocado");
                                    break;
                            }
                            break;
                        }
                    case "3":
                        {
                            turnoff = true;
                            break;
                        }
                    default://error
                        {
                            Console.WriteLine("numero equivocado");
                            break;
                        }
                }
            }
            //final
            channel.Dispose();
            return;
        }
        public static void CrearProducto(GrpcProtos.Product.ProductClient client)
        {
            Console.WriteLine("Escriba el nombre");
            string nameproduct = new(Console.ReadLine());
            
            var createResponse = client.CreateProduct(new CreateProductRequest()
            {
                Name = nameproduct,
            });

            if (createResponse is null)
            {
                Console.WriteLine("Imposible crear producto");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa.");
                return;
            }
        }

        public static void ModificarProducto(GrpcProtos.Product.ProductClient client)
        {
            var getResponse = client.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener los productos");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre del producto quiere modificar");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    var id = getResponse.Items[i].Id;
                    var nombrecomp = getResponse.Items[i].Companyname;
                    var tipoenvase = getResponse.Items[i].ContainerShape.ToString();
                    Console.WriteLine($"Producto {i + 1}");
                    Console.WriteLine($" Nombre: {nombre} ID: {id}");
                    Console.WriteLine($" Nombre de la compania: {nombrecomp} Tipo de envase: {tipoenvase}");
                }
            }
            string nameproduct = new(Console.ReadLine());
            var producttoupdate = client.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == nameproduct);
            if (producttoupdate is null)
            {
                Console.WriteLine("No existe ese producto");
                return;
            }
            Console.WriteLine("Diga la propiedad del producto a modificar");
            Console.WriteLine("1: Nombre. 2:Nombre de la Compania. 3:Tipo del envase");
            int select = Convert.ToInt32(Console.ReadLine());
            switch (select)

            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Diga el nuevo nombre del producto");
                    var nametemp = Console.ReadLine();
                    if (client.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty()).Items.Any(i => i.Name == nametemp))
                    {
                        Console.WriteLine("Ya existe un producto con ese nombre");
                        return;
                    }
                    producttoupdate.Name = nametemp;
                    Console.WriteLine($"Nuevo Nombre:{producttoupdate.Name}");
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Diga el nuevo nombre de la compania");
                    producttoupdate.Companyname = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{producttoupdate.Companyname}");
                    break;
                case 3:
                    Console.WriteLine("Diga el tipo de Envase");
                    Console.WriteLine("1:Botella 2:Caja 3:Ampula 4:Blister 5:Bolsa");
                    select = Convert.ToInt32(Console.ReadLine());
                    switch (select)
                    {
                        case 1:
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Botella;
                            break;
                        case 2:
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Caja;
                            break;
                        case 3:
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Ampula;
                            break;
                        case 4:
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Blister;
                            break;
                        case 5:
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Bolsa;
                            break;
                        default:
                            Console.WriteLine("Numero incorrecto");
                            return;
                    }
                    Console.WriteLine("Tipo de envase modificado");
                    break;

            }


            client.UpdateProduct(producttoupdate);

            var updatedGetResponse = client.GetProduct(new GetRequest() { Id = producttoupdate.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableProductDTO.KindOneofCase.Product &&
               updatedGetResponse.Product.Name == producttoupdate.Name &&
               updatedGetResponse.Product.Companyname == producttoupdate.Companyname &&
               updatedGetResponse.Product.ContainerShape == producttoupdate.ContainerShape)
            {
                Console.WriteLine($"Modificación exitosa.");
                return;
            }
            else
            {
                Console.WriteLine("Hubo un error al modificar");
                return;
            }
        }

        public static void EliminarProducto(GrpcProtos.Product.ProductClient client)
        {
            var getResponse = client.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener los productos");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre del producto quiere eliminar");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    var id = getResponse.Items[i].Id;
                    var nombrecomp = getResponse.Items[i].Companyname;
                    var tipoenvase = getResponse.Items[i].ContainerShape.ToString();
                    Console.WriteLine($"Producto {i + 1}");
                    Console.WriteLine($" Nombre: {nombre} ID: {id}");
                    Console.WriteLine($" Nombre de la compania: {nombrecomp} Tipo de envase: {tipoenvase}");
                };
            }
            string nameproduct = new(Console.ReadLine());
            var producttodelete = client.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == nameproduct);
            if (producttodelete is null)
            {
                Console.WriteLine("No existe ese producto");
                return;
            }
            client.DeleteProduct(new DeleteRequest() { Id = producttodelete.Id });
            var deletedGetResponse = client.GetProduct(new GetRequest() { Id = producttodelete.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableProductDTO.KindOneofCase.Product)
            {
                Console.WriteLine($"Eliminación exitosa.");
                return;
            }
            
        }

        public static void ObtenerProducto(GrpcProtos.Product.ProductClient client)
        {
            var getResponse = client.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener los productos");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre del producto a mostrar de los {getResponse.Items.Count} existentes");
            }
            string nameproduct = new(Console.ReadLine());
            var productsobtained = getResponse.Items.TakeWhile(i => i.Name == nameproduct);// creando un inumerable de todos los productos con ese nombre
            if (productsobtained.Any() == false)
            {
                Console.WriteLine("No existe ese producto");
                return;
            }
            if (productsobtained.Count() == 1)// solo hay un producto con ese nombre
            {
                var productResponse = productsobtained.Single();
                Console.WriteLine($"Obtención exitosa. Nombre: {productResponse.Name}");
                Console.WriteLine($"Nombre de la Compania: {productResponse.Companyname}");
                Console.WriteLine($"Tipo de envase: {productResponse.ContainerShape}");
                return;
            }
            if (productsobtained.Count() > 1)// si hay mas de un producto con ese nombre
            {
                Console.WriteLine("Existen varios productos con ese nombre, por favor teclee el id del producto");
                var productid = Console.ReadLine();
                var productResponse = productsobtained.FirstOrDefault(i => i.Id == productid);
                if (productResponse is null)
                {
                    Console.WriteLine("No existe un producto con ese id");
                    return;
                }
                else
                {
                    Console.WriteLine($"Obtención exitosa. Nombre: {productResponse.Name}");
                    Console.WriteLine($"Nombre de la Compania: {productResponse.Companyname}");
                    Console.WriteLine($"Tipo de envase: {productResponse.ContainerShape}");

                }
            }

            return;
        }

        public static void ObtenerTodosLosProductos(GrpcProtos.Product.ProductClient client)
        {
            var getResponse = client.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener los productos");
                return;
            }
            else
            {
                Console.WriteLine($"Estan son los productos existentes ");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    var id = getResponse.Items[i].Id;
                    var nombrecomp = getResponse.Items[i].Companyname;
                    var tipoenvase = getResponse.Items[i].ContainerShape.ToString();
                    Console.WriteLine($"Producto {i + 1}");
                    Console.WriteLine($" Nombre: {nombre} ID: {id}");
                    Console.WriteLine($" Nombre de la compania: {nombrecomp} Tipo de envase: {tipoenvase}");
                }
            }
        }

        public static void CrearFase(GrpcProtos.Fase.FaseClient client)
        {
            Console.WriteLine("Escriba el nombre");
            string namefase = new(Console.ReadLine());
            Console.WriteLine("Escriba la descripcion");
            string descripcionfase = new(Console.ReadLine());
            Console.WriteLine("Escriba la cantidad de acciones de control que posee");
            int cacant = Convert.ToInt32(Console.ReadLine());
            
            GrpcProtos.CreateFaseRequest temp = new();
            for (int i = 0; i < cacant; i++)
            {
                
                Console.WriteLine("Escriba el nombre de la accion de control");
                string? nameac = Console.ReadLine();
                
                Console.WriteLine("Escriba la cantidad de la accion de control");
                int cantac = Convert.ToInt32(Console.ReadLine());
                
                Console.WriteLine("Escriba la unidad de medida de la accion de control");
                string? umac = Console.ReadLine();
                var temporalCA = new GrpcProtos.ControlAction()
                {
                    ActionName = nameac,
                    Amount = cantac,
                    Measureunit = umac
                };
               
                temp.Actionlist.Add(temporalCA);
            }//para crear la lista de acciones de control
            temp.Name = namefase;
            temp.Description = descripcionfase;
            
            var createResponse = client.CreateFase(temp) ;

            if (createResponse is null)
            {
                Console.WriteLine("Imposible crear fase");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa.");
                return;
            }
        }

        public static void ModificarFase(GrpcProtos.Fase.FaseClient client)
        {
            var getResponse = client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las fases");
                return;
            }
            else
            {
                Console.WriteLine($"Fases existentes");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    Console.WriteLine($"Fase {i + 1} Nombre: {nombre} ");

                };
            }
            Console.WriteLine("Diga el nombre de la fase a modificar");
            string namefase = new(Console.ReadLine());
            var fasetoupdate = client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == namefase);
            if (fasetoupdate is null)
            {
                Console.WriteLine("No existe esa fase");
                return;
            }
            Console.WriteLine("Diga la propiedad de la fase a modificar");
            Console.WriteLine("1: Nombre. 2:Descripcion. 3:Duracion 4:Acciones de control");
            switch (Console.ReadLine())

            {
                case "1":
                    Console.WriteLine("Diga el nuevo nombre de la fase");
                    fasetoupdate.Name = Console.ReadLine();
                    if (client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty()).Items.Any(i =>i.Name == fasetoupdate.Name))
                    {
                        Console.WriteLine("Ya existe una fase con ese nombre");
                        return;
                    }
                    Console.WriteLine($"Nuevo Nombre:{fasetoupdate.Name}");
                    break;
                case "2":
                    Console.WriteLine("Diga la nueva descripcion");
                    fasetoupdate.Description = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{fasetoupdate.Description}");
                    break;
                case "3":
                    Console.WriteLine("Diga la duracion");
                    fasetoupdate.Duration = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Nueva duracion {fasetoupdate.Duration}");
                    break;
                case "4":
                    Console.WriteLine("Desea crear una nueva accion de control(Presione 1) o modoficar una existente Presione (2)");
                    var select = Console.ReadLine();
                    switch (select)
                    {
                        case "1":
                            Console.WriteLine("Escriba el nombre de la accion de control");
                            string? namenewac = Console.ReadLine();

                            Console.WriteLine("Escriba la cantidad de la accion de control");
                            int cantnewac = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Escriba la unidad de medida de la accion de control");
                            string? umnewac = Console.ReadLine();
                            var newCA = new GrpcProtos.ControlAction()
                            {
                                ActionName = namenewac,
                                Amount = cantnewac,
                                Measureunit = umnewac
                            };
                            fasetoupdate.Actionlist.Add(newCA);
                            break;
                        case "2":
                            Console.WriteLine("Diga el numero de la accion de control a modificar");
                            var selectca = Convert.ToInt32(Console.ReadLine());
                            if (selectca > fasetoupdate.Actionlist.Count)
                            {
                                Console.WriteLine("Error al teclear, no existen tantas acciones de control");
                                return;
                            }
                            Console.WriteLine("Escriba el nombre de la accion de control");
                            string? nameac = Console.ReadLine();

                            Console.WriteLine("Escriba la cantidad de la accion de control");
                            int cantac = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Escriba la unidad de medida de la accion de control");
                            string? umac = Console.ReadLine();
                            var temporalCA = new GrpcProtos.ControlAction()
                            {
                                ActionName = nameac,
                                Amount = cantac,
                                Measureunit = umac
                            };
                            fasetoupdate.Actionlist.RemoveAt(selectca-1);
                            fasetoupdate.Actionlist.Insert(selectca-1, temporalCA);
                            break;
                        default:
                            Console.WriteLine("Error al teclear");
                            return;
                            
                    }
                   break;

                default:
                    Console.WriteLine("Error al teclear");
                    return;
                    
            }


            client.UpdateFase(fasetoupdate);

            var updatedGetResponse = client.GetFase(new GetRequest() { Id = fasetoupdate.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableFaseDTO.KindOneofCase.Fase &&
                updatedGetResponse.Fase.Description == fasetoupdate.Description &&
                updatedGetResponse.Fase.Duration == fasetoupdate.Duration &&
               updatedGetResponse.Fase.Name == fasetoupdate.Name &&
               updatedGetResponse.Fase.Actionlist.SequenceEqual(fasetoupdate.Actionlist)
                )
            {
                Console.WriteLine($"Modificación exitosa.");
                return;
            }
            else
            {
                Console.WriteLine("Hubo un error al modificar");
                return;
            }
        }

        public static void EliminarFase(GrpcProtos.Fase.FaseClient client)
        {
            var getResponse = client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las fases");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre de la fase que quiere eliminar de los {getResponse.Items.Count} existentes");
            }
            string namefase = new(Console.ReadLine());
            var fasetodelete = client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == namefase);
            if (fasetodelete is null)
            {
                Console.WriteLine("No existe esa fase");
                return;
            }
            client.DeleteFase(new DeleteRequest() { Id = fasetodelete.Id });
            var deletedGetResponse = client.GetFase(new GetRequest() { Id = fasetodelete.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableFaseDTO.KindOneofCase.Fase)
            {
                Console.WriteLine($"Eliminación exitosa.");
                return;
            }

        }


        public static void ObtenerFase(GrpcProtos.Fase.FaseClient client)
        {
            var getResponse = client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las Fases");
                return;
            }
            else
            {
                Console.WriteLine($"Fases existentes");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    Console.WriteLine($"Fase {i + 1} Nombre: {nombre} ");
                    
                }
            }
            Console.WriteLine("Diga el nombre de la fase a modificar");
            string namefase = new(Console.ReadLine());
            var fasesobtained = getResponse.Items.Where(i => i.Name == namefase);// creando un inumerable de todas las fases con ese nombre
            if (fasesobtained.Any() == false)
            {
                Console.WriteLine("No existe esa fase");
                return;
            }
            if (fasesobtained.Count() == 1)// solo hay una fase con ese nombre
            {
                var faseResponse = fasesobtained.Single();
                Console.WriteLine($"Obtención exitosa. Nombre: {faseResponse.Name}");
                Console.WriteLine($"Descripcion: {faseResponse.Description}");
                Console.WriteLine($"Duracion: {faseResponse.Duration}");
                Console.WriteLine($"{faseResponse.Actionlist.Count} Acciones de control:");
                for (int i = 0; i < faseResponse.Actionlist.Count; i++)
                {
                    Console.WriteLine($"Accion de control {i}");
                    Console.WriteLine($"{faseResponse.Actionlist[i].ActionName} {faseResponse.Actionlist[i].Amount} {faseResponse.Actionlist[i].Measureunit}");
                }
                return;
            }
            if (fasesobtained.Count() > 1)// si hay mas de una fase con ese nombre
            {
                Console.WriteLine("Existen varias fases con ese nombre, por favor teclee el id de la fase");
                var faseid = Console.ReadLine();
                var faseResponse = fasesobtained.FirstOrDefault(i => i.Id == faseid);
                if (faseResponse is null)
                {
                    Console.WriteLine("No existe una fase con ese id");
                    return;
                }
                else
                {
                    Console.WriteLine($"Obtención exitosa. Nombre: {faseResponse.Name}");
                    Console.WriteLine($"Descripcion: {faseResponse.Description}");
                    Console.WriteLine($"Duracion: {faseResponse.Duration}");
                    Console.WriteLine($"Acciones de control:");
                    for (int i = 0; i < faseResponse.Actionlist.Count; i++)
                    {
                        Console.WriteLine($"Accion de control {i+1}");
                        Console.WriteLine($"{faseResponse.Actionlist[i].ActionName}{faseResponse.Actionlist[i].Amount}{faseResponse.Actionlist[i].Measureunit}");
                    }
                }
            }

            return;
        }

        public static void ObtenerTodasLasFases(GrpcProtos.Fase.FaseClient client)
        {
            var getResponse = client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las fases");
                return;
            }
            else
            {
                Console.WriteLine($"Estan son las fases existentes ");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    var descripcion = getResponse.Items[i].Description;
                    var duracion = getResponse.Items[i].Duration;
                    var id = getResponse.Items[i].Id;
                    Console.WriteLine($"Fase {i + 1} Nombre: {nombre} ");
                    Console.WriteLine($"ID: {id} Duracion: {duracion}");
                    Console.WriteLine($"Descripcion: {descripcion}");
                }
            }
        }

        public static void CrearOperacion(GrpcProtos.Operation.OperationClient client)
        {
            Console.WriteLine("Escriba el nombre");
            string nameop = new(Console.ReadLine());
            Console.WriteLine("Escriba la descripcion");
            string descripcionop = new(Console.ReadLine());
            
            
            

            var createResponse = client.CreateOperation(new CreateOperationRequest()
            {
                Name = nameop,
                Description = descripcionop,
           
            });

            if (createResponse is null)
            {
                Console.WriteLine("Imposible crear operacion");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa.");
                return;
            }
        }

        public static void ModificarOperacion(GrpcProtos.Operation.OperationClient client, GrpcProtos.Fase.FaseClient faseclient)
        {
            var getResponse = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las operaciones");
                return;
            }
            else
            {
                Console.WriteLine($"Operaciones existentes");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    Console.WriteLine($"Operacion {i + 1} Nombre: {nombre} ");

                };
                Console.WriteLine($"Diga el nombre de la operacion que quiere modificar");
            }
            string nameop = new(Console.ReadLine());
            var operationtoupdate = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == nameop);
            if (operationtoupdate is null)
            {
                Console.WriteLine("No existe esa operacion");
                return;
            }
            Console.WriteLine("Diga la propiedad de la operacion a modificar");
            Console.WriteLine("1: Nombre. 2:Descripcion. 3:Duracion 4: Lista de fases");
            switch (Console.ReadLine())

            {
                case "1":
                    Console.WriteLine("Diga el nuevo nombre de la operacion");
                    operationtoupdate.Name = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{operationtoupdate.Name}");
                    break;
                case "2":
                    Console.WriteLine("Diga la nueva descripcion");
                    operationtoupdate.Description = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{operationtoupdate.Description}");
                    break;
                case "3":
                    Console.WriteLine("Diga la nueva unidad sobre la que se realizara la operacion");
                    operationtoupdate.Unityname = Console.ReadLine();
                    Console.WriteLine($"Nueva Unidad {operationtoupdate.Unityname}");
                    break;
                case "4":
                    Console.WriteLine("Desea adjuntar(Presione 1) o eliminar(Presione 2) una fase de la lista");
                    switch(Console.ReadLine())
                    {
                        case "1":
                            var getResponseFase = faseclient.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty());
                            if (getResponseFase.Items is null)
                            {
                                Console.WriteLine("Error al obtener las fases");
                                return;
                            }
                            Console.WriteLine($"Fases existentes");
                            for (int i = 0; i < getResponseFase.Items.Count; i++)
                            {
                                var nombre = getResponseFase.Items[i].Name;
                                Console.WriteLine($"Fase {i + 1} Nombre: {nombre} ");
                            };
                            Console.WriteLine("Cual fase desea adjuntar a la lista");
                            var faseadjuntarname = Console.ReadLine();
                            var faseadjuntar = getResponseFase.Items.FirstOrDefault(i => i.Name == faseadjuntarname);
                            operationtoupdate.Faselist.Add(faseadjuntar);
                            break;
                        case "2":
                            Console.WriteLine($"Fases existentes en la lista");
                            for (int i = 0; i < operationtoupdate.Faselist.Count; i++)
                            {
                                var nombre = operationtoupdate.Faselist[i].Name;
                                Console.WriteLine($"Fase {i + 1} Nombre: {nombre} ");
                                Console.WriteLine("Cual fase desea eliminar de la lista");
                                var fasedeletename = Console.ReadLine();
                                var fasedelete = operationtoupdate.Faselist.FirstOrDefault(i=>i.Name == fasedeletename);
                                operationtoupdate.Faselist.Remove(fasedelete);
                            };
                            break;
                        default:
                            Console.WriteLine("Error de tecleo");
                            return;
                    }
                    break;

                default:
                    Console.WriteLine("Error");
                    return;

            }


            client.UpdateOperation(operationtoupdate);

            var updatedGetResponse = client.GetOperation(new GetRequest() { Id = operationtoupdate.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableOperationDTO.KindOneofCase.Operation &&
                updatedGetResponse.Operation.Unityname == operationtoupdate.Unityname &&
                updatedGetResponse.Operation.Id == operationtoupdate.Id&&
                updatedGetResponse.Operation.Name == operationtoupdate.Name
                )
            {
                Console.WriteLine($"Modificación exitosa.");
                return;
            }
            else
            {
                Console.WriteLine("Hubo un error al modificar");
                return;
            }
        }

        public static void EliminarOperacion(GrpcProtos.Operation.OperationClient client)
        {
            var getResponse = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las operaciones");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre de la operacion que quiere eliminar de los {getResponse.Items.Count} existentes");
            }
            string nameoperation = new(Console.ReadLine());
            var operationtodelete = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == nameoperation);
            if (operationtodelete is null)
            {
                Console.WriteLine("No existe esa operacion");
                return;
            }
            client.DeleteOperation(new DeleteRequest() { Id = operationtodelete.Id });
            var deletedGetResponse = client.GetOperation(new GetRequest() { Id = operationtodelete.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableOperationDTO.KindOneofCase.Operation)
            {
                Console.WriteLine($"Eliminación exitosa.");
                return;
            }
        }

        public static void ObtenerOperacion(GrpcProtos.Operation.OperationClient client)
        {
            var getResponse = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las Operaciones");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre de la operacion a mostrar sus detalles");
                Console.WriteLine($"Operaciones existentes");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    Console.WriteLine($"Operacion {i + 1} Nombre: {nombre}");
                }
                string nameoperation = new(Console.ReadLine());
                var opsobtained = getResponse.Items.Where(i => i.Name == nameoperation);// creando un inumerable de todas las fases con ese nombre
                if (opsobtained.Any() == false)
                {
                    Console.WriteLine("No existe esa operacion");
                    return;
                }
                if (opsobtained.Count() == 1)// solo hay una fase con ese nombre
                {
                    var operationResponse = opsobtained.Single();
                    Console.WriteLine($"Obtención exitosa. Nombre: {operationResponse.Name}");
                    Console.WriteLine($"Descripcion: {operationResponse.Description}");
                    Console.WriteLine($"Nombre de la unidad: {operationResponse.Unityname}");
                    for (int i = 0; i < operationResponse.Faselist.Count(); i++)
                    {
                        var nombre = operationResponse.Faselist[i].Name;
                        Console.WriteLine($"Fase {i + 1} Nombre: {nombre} "); ;
                    }


                    return;
                }
                if (opsobtained.Count() > 1)// si hay mas de una fase con ese nombre
                {
                    Console.WriteLine("Existen varias operaciones con ese nombre, por favor teclee el id de la operacion");
                    var opid = Console.ReadLine();
                    var opResponse = opsobtained.FirstOrDefault(i => i.Id == opid);
                    if (opResponse is null)
                    {
                        Console.WriteLine("No existe una operacion con ese id");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Obtención exitosa. Nombre: {opResponse.Name}");
                        Console.WriteLine($"Descripcion: {opResponse.Description}");
                        Console.WriteLine($"Nombre de la unidad: {opResponse.Unityname}");

                    }
                }

                return;
            }
        }

        public static void ObtenerTodasLasOperaciones(GrpcProtos.Operation.OperationClient client)
        {
            var getResponse = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las operaciones");
                return;
            }
            else
            {
                Console.WriteLine($"Estan son las operaciones existentes");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    var id = getResponse.Items[i].Id;
                    var descrip = getResponse.Items[i].Description;
                    var unidad = getResponse.Items[i].Unityname;
                    Console.WriteLine($"Operacion {i + 1} Nombre: {nombre}");
                    Console.WriteLine($"ID: {id} Unidad sobre la que actua: {unidad}");
                    Console.WriteLine($"Descripcion: {descrip}");
                }
            }
        }

        public static void CrearReceta(GrpcProtos.Recipe.RecipeClient client, GrpcProtos.Product.ProductClient clientproduct)
        {
            var getResponse = clientproduct.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty());
            var productResponse = new ProductDTO();
            

            if (getResponse.Items is null)
            {
                Console.WriteLine("Error");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre del producto objetivo la receta");
                Console.WriteLine($"Estan son los productos existentes ");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var nombre = getResponse.Items[i].Name;
                    Console.WriteLine($"Fase {i + 1} Nombre: {nombre} ");
                }
            }
            string nameproduct = new(Console.ReadLine());
            var productsobtained = getResponse.Items.TakeWhile(i => i.Name == nameproduct);// creando un inumerable de todos los productos con ese nombre
            if (productsobtained.Any() == false)
            {
                Console.WriteLine("No existe ese producto");
                return;
            }
            if (productsobtained.Count() == 1)// solo hay un producto con ese nombre
            {
                productResponse = productsobtained.Single();
            }
            if (productsobtained.Count() > 1)// si hay mas de un producto con ese nombre
            {
                Console.WriteLine("Existen varios productos con ese nombre, por favor teclee el id del producto");
                var productid = Console.ReadLine();
                productResponse = productsobtained.FirstOrDefault(i => i.Id == productid);
                if (productResponse is null)
                {
                    Console.WriteLine("No existe un producto con ese id");
                    return;
                }
                
            }
            GrpcProtos.CreateRecipeRequest temp = new();
            temp.Producttomake = productResponse;
            var createResponse = client.CreateRecipe(temp);

            if (createResponse is null)
            {
                Console.WriteLine("Imposible crear receta");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa.");
                return;
            }
        }

        public static void ModificarReceta(GrpcProtos.Recipe.RecipeClient client, GrpcProtos.Product.ProductClient clientproduct)
        {
            var getResponse = client.GetAllRecipes(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las recetas");
                return;
            }
            else
            {
                Console.WriteLine($"Estan son las recetas existentes y el producto al que estan destinadas ");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var item = getResponse.Items[i].Producttomake;
                    Console.WriteLine($"Receta {i+1} Producto: {item.Name}");
                }
                Console.WriteLine("Elija el numero de receta a modificar"); 
            }
            int recipenumber = Convert.ToInt32(Console.ReadLine());
            var recipetoupdate = getResponse.Items[recipenumber];
            if (recipetoupdate is null)
            {
                Console.WriteLine("No existe esa receta");
                return;
            }
            Console.WriteLine("Tipo de modificacion");
            Console.WriteLine("1: Producto Destino. 2:Lista de Operaciones.");
            switch (Console.Read())

            {
                case '1':
                    var productsResponse = clientproduct.GetAllProducts(new Google.Protobuf.WellKnownTypes.Empty());
                    Console.WriteLine($"Estan son los productos existentes");
                    for (int i = 0; i < productsResponse.Items.Count; i++)
                    {
                        var nombre = productsResponse.Items[i].Name;
                        Console.WriteLine($"Producto {i + 1}");
                        Console.WriteLine($" Nombre: {nombre} ");    
                    }
                    Console.WriteLine($"Diga el nombre del nuevo producto destino ");
                    string? productdestiny = Console.ReadLine();
                    recipetoupdate.Producttomake = productsResponse.Items.FirstOrDefault(i => i.Name == productdestiny);
                    if (recipetoupdate.Producttomake.Name == productdestiny)
                    {
                        Console.WriteLine("Modificacion exitosa");

                    }
                    else Console.WriteLine("ERROR");
                    break;
                case '2':
                    Console.WriteLine("No Implementado");
                    
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;

            }


            client.UpdateRecipe(recipetoupdate);

            var updatedGetResponse = client.GetRecipe(new GetRequest() { Id = recipetoupdate.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableRecipeDTO.KindOneofCase.Recipe &&
                updatedGetResponse.Recipe == recipetoupdate)
            {
                Console.WriteLine($"Modificación exitosa.");
                return;
            }
            else
            {
                Console.WriteLine("Hubo un error al modificar");
                return;
            }
        }

        public static void EliminarReceta(GrpcProtos.Recipe.RecipeClient client)
        {
            var getResponse = client.GetAllRecipes(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las recetas");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el id de la receta que quiere eliminar de las {getResponse.Items.Count} existentes");
            }
            string idrecipe = new(Console.ReadLine());
            var recipetodelete = getResponse.Items.FirstOrDefault(i => i.Id == idrecipe);
            if (recipetodelete is null)
            {
                Console.WriteLine("No existe esa receta");
                return;
            }
            client.DeleteRecipe(new DeleteRequest() { Id = recipetodelete.Id });
            var deletedGetResponse = client.GetRecipe(new GetRequest() { Id = recipetodelete.Id });
            if (deletedGetResponse is null ||
                deletedGetResponse.KindCase != NullableRecipeDTO.KindOneofCase.Recipe)
            {
                Console.WriteLine($"Eliminación exitosa.");
                return;
            }
        }

        public static void ObtenerReceta(GrpcProtos.Recipe.RecipeClient client)
        {

        }

        public static void ObtenerTodasLasRecetas(GrpcProtos.Recipe.RecipeClient client)
        {
            var getResponse = client.GetAllRecipes(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las recetas");
                return;
            }
            else
            {
                Console.WriteLine($"Estan son las recetas existentes y el producto al que estan destinadas ");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var item = getResponse.Items[i].Producttomake.Name;
                    var id = getResponse.Items[i].Id;
                    Console.WriteLine($"Receta {i + 1} ID:{id} Producto: {item}");
                }
            }
        }
        public static void ValidarReceta(GrpcProtos.Recipe.RecipeClient client)
        {
            var getResponse = client.GetAllRecipes(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las recetas");
                return;
            }
            else
            {
                Console.WriteLine($"Estan son las recetas existentes y el producto al que estan destinadas ");
                for (int i = 0; i < getResponse.Items.Count; i++)
                {
                    var item = getResponse.Items[i].Producttomake;
                    Console.WriteLine($"Receta {i + 1} Producto: {item.Name}");
                }
                Console.WriteLine("Elija el numero de receta a validar");
            }
            int recipenumber = Convert.ToInt32(Console.ReadLine());
            var recipetovalidate = getResponse.Items[recipenumber-1];// -1 xq empieza desde 0
            if (recipetovalidate is null)
            {
                Console.WriteLine("No existe esa receta");
                return;
            }
            Console.WriteLine("Introduzca los datos de validacion.");
            Console.WriteLine("Nombre del experto");
            var expert = Console.ReadLine();
            var validatedate = DateTime.Now.ToString();
            client.ValidateRecipe(new ValidateRecipeRequest()
            {
                Expert = expert,
                Recipe = recipetovalidate,
                Validationdate = validatedate
            });
            var updatedGetResponse = client.GetRecipe(new GetRequest() { Id = recipetovalidate.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableRecipeDTO.KindOneofCase.Recipe &&
                updatedGetResponse.Recipe.Validationdate == validatedate &&
                updatedGetResponse.Recipe.Expertname == expert
                )
            {
                Console.WriteLine($"Modificación exitosa.");
                return;
            }
            else
            {
                Console.WriteLine("Hubo un error al modificar");
                return;
            }

        }

        public static void ObtenerProductodeReceta(GrpcProtos.Recipe.RecipeClient client)
        {
            Console.WriteLine($"Introduzca el id de la receta de la cual desea conocer el id de su porducto");
            var recipeid = Console.ReadLine();

            var getResponse = client.GetIdOfProductToMake(new GetRequest() { Id = recipeid });
            if (getResponse is not null &&
                    getResponse.KindCase == NullableProductID.KindOneofCase.Producttomakeid)
            {
                Console.WriteLine($"El ID del producto es: {getResponse.Producttomakeid}");
                return;
            }
            else
            {
                Console.WriteLine($"La receta no existe");

            }
        }
    }
        
}

    
