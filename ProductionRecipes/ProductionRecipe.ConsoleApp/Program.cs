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
                Console.WriteLine("Menu Principal");
                Console.WriteLine("Presione 1 para modificar productos, 2 para recetas y sus derivados, 3 Salir");

                int keypressed = Convert.ToInt32(Console.ReadLine());

                switch (keypressed)
                {
                    case 1://modificando productos
                        {
                            var client = new ProductionRecipes.GrpcProtos.Product.ProductClient(channel);
                            Console.Clear();
                            Console.WriteLine("1: Crear un Producto  2: Modificar un Producto");
                            Console.WriteLine("3: Borrar un Producto 4: Obtener Informacion de un producto");
                            Console.WriteLine("5: Obtener el total de Productos");

                            int producttask = Convert.ToInt32(Console.ReadLine());
                            switch (producttask)
                            {
                                case 1:
                                    {
                                        CrearProducto(client);
                                        break;

                                    }//crear producto
                                case 2:
                                    {
                                        ModificarProducto(client);
                                        break;

                                    }//Modificar el pproducto
                                case 3:
                                    {
                                        EliminarProducto(client);
                                        break;

                                    }//Borrar producto
                                case 4:
                                    {
                                        ObtenerProducto(client);
                                        break;
                                    }//Obtener informacion de un producto
                                case 5:
                                    {
                                        ObtenerTodosLosProductos(client);
                                        break;

                                    }//Obtener el total de productos
                                default:
                                    Console.WriteLine("Numero equivocado");
                                    return;
                            }
                            break;
                        }
                    case 2://modificando recetas y derivados
                        {
                            Console.Clear();
                            Console.WriteLine("1: Recetas  2: Operaciones");
                            Console.WriteLine("3: Fases");


                            int select = Convert.ToInt32(Console.ReadLine());
                            switch (select)
                            {
                                case 1://Trabajo con Recetas
                                    {
                                        var client = new ProductionRecipes.GrpcProtos.Recipe.RecipeClient(channel);
                                        var clientproduct = new ProductionRecipes.GrpcProtos.Product.ProductClient(channel);
                                        Console.Clear();
                                        Console.WriteLine("1: Crear una Receta  2: Modificar una Receta");
                                        Console.WriteLine("3: Borrar una Receta 4: Obtener Informacion de una Receta");
                                        Console.WriteLine("5: Obtener el total de Recetas 6: Validar una Receta");
                                        Console.WriteLine("7: Obtener producto de una receta");
                                        int recipetask = Convert.ToInt32(Console.ReadLine());
                                        switch (recipetask)
                                        {
                                            case 1:
                                                {
                                                    CrearReceta(client, clientproduct);
                                                    break;
                                                }//crearreceta
                                            case 2:
                                                {
                                                    ModificarReceta(client, clientproduct);
                                                    break;
                                                }//modificarreceta
                                            case 3:
                                                {
                                                    EliminarReceta(client);
                                                    break;
                                                }//eliminarreceta
                                            case 4:
                                                {
                                                    ObtenerReceta(client);
                                                    break;
                                                }//obtenerreceta
                                            case 5:
                                                {
                                                    ObtenerTodasLasRecetas(client);
                                                    break;
                                                }//obtenertodaslasrecetas
                                            case 6:
                                                {
                                                    ValidarReceta(client);
                                                    break;
                                                }//validarreceta
                                            case 7:
                                                {
                                                    ObtenerProductodeReceta(client);
                                                    break;
                                                }//obtenerproductodereceta
                                        }
                                        break;
                                    }
                                case 2://Trabajo con Operaciones
                                    {
                                        var client = new ProductionRecipes.GrpcProtos.Operation.OperationClient(channel);
                                        Console.Clear();
                                        Console.WriteLine("1: Crear una Operacion  2: Modificar una Operacion");
                                        Console.WriteLine("3: Borrar una Operacion 4: Obtener Informacion de una Operacion");
                                        Console.WriteLine("5: Obtener el total de Operaciones");
                                        int optask = Convert.ToInt32(Console.ReadLine());
                                        switch (optask)
                                        {
                                            case 1:
                                                {
                                                    CrearOperacion(client);
                                                    break;
                                                }//crearOp
                                            case 2:
                                                {
                                                    ModificarOperacion(client);
                                                    break;
                                                }//modificarOp
                                            case 3:
                                                {
                                                    EliminarOperacion(client);
                                                    break;
                                                }//eliminarOp
                                            case 4:
                                                {
                                                    ObtenerOperacion(client);
                                                    break;
                                                }//obtenerOp
                                            case 5:
                                                {
                                                    ObtenerTodasLasOperaciones(client);
                                                    break;
                                                }//obtenertodaslasOp
                                        }
                                        break;
                                    }
                                case 3://Trabajo con fases
                                    {
                                        var client = new ProductionRecipes.GrpcProtos.Fase.FaseClient(channel);
                                        Console.Clear();
                                        Console.WriteLine("1: Crear una Fase  2: Modificar una Fase");
                                        Console.WriteLine("3: Borrar una Fase 4: Obtener Informacion de una Fase");
                                        Console.WriteLine("5: Obtener el total de Fases");
                                        int fasetask = Convert.ToInt32(Console.ReadLine());
                                        switch (fasetask)
                                        {
                                            case 1:
                                                {
                                                    CrearFase(client);
                                                    break;
                                                }//crearfase
                                            case 2:
                                                {
                                                    ModificarFase(client);
                                                    break;
                                                }//modificarfase
                                            case 3:
                                                {
                                                    EliminarFase(client);
                                                    break;
                                                }//eliminarfase
                                            case 4:
                                                {
                                                    ObtenerFase(client);
                                                    break;
                                                }//obtenerfase
                                            case 5:
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
                    case 3:
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
            var select = Console.Read();
            switch (select)

            {
                case '1':
                    Console.Clear();
                    Console.WriteLine("Diga el nuevo nombre del producto");
                    var nametemp = Console.ReadLine();
                    producttoupdate.Name = nametemp;
                    Console.WriteLine($"Nuevo Nombre:{producttoupdate.Name}");
                    break;
                case '2':
                    Console.Clear();
                    Console.WriteLine("Diga el nuevo nombre de la compania");
                    producttoupdate.Companyname = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{producttoupdate.Companyname}");
                    break;
                case '3':
                    Console.WriteLine("Diga el tipo de Envase");
                    Console.WriteLine("1:Botella 2:Caja 3:Ampula 4:Blister 5:Bolsa");
                    switch (Console.Read())
                    {
                        case '1':
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Botella;
                            break;
                        case '2':
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Caja;
                            break;
                        case '3':
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Ampula;
                            break;
                        case '4':
                            producttoupdate.ContainerShape = GrpcProtos.ContainerShape.Blister;
                            break;
                        case '5':
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
                updatedGetResponse.Product == producttoupdate)
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
                Console.WriteLine($"Diga el nombre del producto quiere eliminar de los {getResponse.Items.Count} existentes");
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

            Google.Protobuf.Collections.RepeatedField<GrpcProtos.ControlAction> controlActions = new();
            for (int i = 0; i < cacant; i++)
            {
                
                Console.WriteLine("Escriba el nombre de la accion de control");
                string? nameac = Console.ReadLine();
                controlActions[i].ActionName = nameac;
                Console.WriteLine("Escriba la cantidad de la accion de control");
                int cantac = Convert.ToInt32(Console.ReadLine());
                controlActions[i].Amount = cantac;
                Console.WriteLine("Escriba la unidad de medida de la accion de control");
                string? umac = Console.ReadLine();
                controlActions[i].Measureunit = umac;
            }//para crear la lista de acciones de control

            var createResponse = client.CreateFase(new CreateFaseRequest()
            {
                Name = namefase,
                Description = descripcionfase
                //actionlist
            }) ;

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
                Console.WriteLine($"Diga el nombre de la fase que quiere modificar de las {getResponse.Items.Count} existentes");
            }
            string namefase = new(Console.ReadLine());
            var fasetoupdate = client.GetAllFases(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == namefase);
            if (fasetoupdate is null)
            {
                Console.WriteLine("No existe esa fase");
                return;
            }
            Console.WriteLine("Diga la propiedad de la fase a modificar");
            Console.WriteLine("1: Nombre. 2:Descripcion. 3:Duracion");
            switch (Console.Read())

            {
                case '1':
                    Console.WriteLine("Diga el nuevo nombre de la fase");
                    fasetoupdate.Name = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{fasetoupdate.Name}");
                    break;
                case '2':
                    Console.WriteLine("Diga la nueva descripcion");
                    fasetoupdate.Description = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{fasetoupdate.Description}");
                    break;
                case '3':
                    Console.WriteLine("Diga la duracion");
                    fasetoupdate.Duration = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Nueva duracion {fasetoupdate.Duration}");
                    break;

            }


            client.UpdateFase(fasetoupdate);

            var updatedGetResponse = client.GetFase(new GetRequest() { Id = fasetoupdate.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableFaseDTO.KindOneofCase.Fase &&
                updatedGetResponse.Fase == fasetoupdate)
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
                Console.WriteLine($"Diga el nombre de la fase a mostrar de las {getResponse.Items.Count} existentes");
            }
            string namefase = new(Console.ReadLine());
            var fasesobtained = getResponse.Items.TakeWhile(i => i.Name == namefase);// creando un inumerable de todas las fases con ese nombre
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
            
            //lista de fases???
            
            

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

        public static void ModificarOperacion(GrpcProtos.Operation.OperationClient client)
        {
            var getResponse = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty());
            if (getResponse.Items is null)
            {
                Console.WriteLine("Error al obtener las operaciones");
                return;
            }
            else
            {
                Console.WriteLine($"Diga el nombre de la operacion que quiere modificar de las {getResponse.Items.Count} existentes");
            }
            string nameop = new(Console.ReadLine());
            var operationtoupdate = client.GetAllOperations(new Google.Protobuf.WellKnownTypes.Empty()).Items.FirstOrDefault(i => i.Name == nameop);
            if (operationtoupdate is null)
            {
                Console.WriteLine("No existe esa operacion");
                return;
            }
            Console.WriteLine("Diga la propiedad de la operacion a modificar");
            Console.WriteLine("1: Nombre. 2:Descripcion. 3:Duracion");
            switch (Console.Read())

            {
                case '1':
                    Console.WriteLine("Diga el nuevo nombre de la fase");
                    operationtoupdate.Name = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{operationtoupdate.Name}");
                    break;
                case '2':
                    Console.WriteLine("Diga la nueva descripcion");
                    operationtoupdate.Description = Console.ReadLine();
                    Console.WriteLine($"Nuevo Nombre:{operationtoupdate.Description}");
                    break;
                case '3':
                    Console.WriteLine("Diga la nueva unidad sobre la que se realizara la operacion");
                    operationtoupdate.Unityname = Console.ReadLine();
                    Console.WriteLine($"Nueva Unidad {operationtoupdate.Unityname}");
                    break;

            }


            client.UpdateOperation(operationtoupdate);

            var updatedGetResponse = client.GetOperation(new GetRequest() { Id = operationtoupdate.Id });
            if (updatedGetResponse is not null &&
                updatedGetResponse.KindCase == NullableOperationDTO.KindOneofCase.Operation &&
                updatedGetResponse.Operation == operationtoupdate)
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
                Console.WriteLine($"Diga el nombre de la operacion a mostrar de las {getResponse.Items.Count} existentes");
            }
            string nameoperation = new(Console.ReadLine());
            var opsobtained = getResponse.Items.TakeWhile(i => i.Name == nameoperation);// creando un inumerable de todas las fases con ese nombre
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

            var createResponse = client.CreateRecipe(new CreateRecipeRequest()
            {
                Producttomake = productResponse
                 //lista de operaciones???

            });

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
                    var item = getResponse.Items[i].Producttomake.Name;
                    Console.WriteLine($"Receta {i+1} Producto: {item}");
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

    
