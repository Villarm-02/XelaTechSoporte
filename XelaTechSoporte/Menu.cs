using System;
using System.Collections.Generic;

namespace XelaTechSoporte
{
    public class Menu
    {
        private Sistema sistema;
        private List<string> resultadosBusquedas = new List<string>();

        public Menu(Sistema sistemaRecibido)
        {
            sistema = sistemaRecibido;
        }

        private string LeerLinea()
        {
            string linea = Console.ReadLine();
            if (linea == null)
            {
                Environment.Exit(0);
            }
            return linea;
        }

        private int LeerEntero(string mensaje)
        {
            int numero;
            Console.Write(mensaje);
            while (int.TryParse(LeerLinea(), out numero) == false)
            {
                Console.Write("Debe escribir un numero entero: ");
            }
            return numero;
        }

        private double LeerDouble(string mensaje)
        {
            double numero;
            Console.Write(mensaje);
            while (double.TryParse(LeerLinea(), out numero) == false)
            {
                Console.Write("Debe escribir un numero: ");
            }
            return numero;
        }

        private string LeerTexto(string mensaje)
        {
            Console.Write(mensaje);
            string texto = LeerLinea();
            while (texto.Trim() == "")
            {
                Console.Write("Este dato no puede estar vacio: ");
                texto = LeerLinea();
            }
            return texto.Trim();
        }

        private bool LeerSiNo(string mensaje)
        {
            string respuesta = LeerTexto(mensaje + " (s/n): ").ToLower();
            while (respuesta != "s" && respuesta != "n")
            {
                respuesta = LeerTexto("Escriba s o n: ").ToLower();
            }
            if (respuesta == "s")
            {
                return true;
            }
            return false;
        }

        private string ElegirOpcion(string titulo, string[] opciones)
        {
            Console.WriteLine(titulo);
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {opciones[i]}");
            }
            int opcion = LeerEntero("Opcion: ");
            while (opcion < 1 || opcion > opciones.Length)
            {
                opcion = LeerEntero($"Escriba un numero del 1 al {opciones.Length}: ");
            }
            return opciones[opcion - 1];
        }

        private bool LeerOrdenAscendente()
        {
            string orden = ElegirOpcion("Orden:", new string[] { "Ascendente", "Descendente" });
            if (orden == "Ascendente")
            {
                return true;
            }
            return false;
        }

        private void Pausa()
        {
            Console.WriteLine();
            Console.Write("Presione Enter para continuar...");
            LeerLinea();
        }

        private void Titulo(string texto)
        {
            Console.WriteLine();
            Console.WriteLine("==================================================================");
            Console.WriteLine(" " + texto);
            Console.WriteLine("==================================================================");
        }

        public void MostrarMenu()
        {
            int opcion = 0;
            while (opcion != 11)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("            XELATECH SOPORTE");
                Console.WriteLine("============================================");
                Console.WriteLine(" 1. Gestion de clientes");
                Console.WriteLine(" 2. Gestion de tecnicos");
                Console.WriteLine(" 3. Gestion de equipos");
                Console.WriteLine(" 4. Gestion de solicitudes");
                Console.WriteLine(" 5. Gestion de repuestos");
                Console.WriteLine(" 6. Atencion de colas");
                Console.WriteLine(" 7. Ordenamientos");
                Console.WriteLine(" 8. Busquedas");
                Console.WriteLine(" 9. Reportes");
                Console.WriteLine("10. Generar datos de prueba");
                Console.WriteLine("11. Salir");
                Console.WriteLine("--------------------------------------------");
                opcion = LeerEntero("Seleccione una opcion: ");

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            MenuClientes();
                            break;
                        case 2:
                            MenuTecnicos();
                            break;
                        case 3:
                            MenuEquipos();
                            break;
                        case 4:
                            MenuSolicitudes();
                            break;
                        case 5:
                            MenuRepuestos();
                            break;
                        case 6:
                            MenuColas();
                            break;
                        case 7:
                            MenuOrdenamientos();
                            break;
                        case 8:
                            MenuBusquedas();
                            break;
                        case 9:
                            MostrarReportes();
                            break;
                        case 10:
                            sistema.GenerarDatosPrueba();
                            Pausa();
                            break;
                        case 11:
                            Console.WriteLine("Gracias por usar XelaTech Soporte.");
                            break;
                        default:
                            Console.WriteLine("Opcion no valida.");
                            Pausa();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocurrio un error: " + ex.Message);
                    Pausa();
                }
            }
        }

        private void MenuClientes()
        {
            int opcion = 0;
            while (opcion != 5)
            {
                Titulo("GESTION DE CLIENTES");
                Console.WriteLine(" 1. Registrar cliente");
                Console.WriteLine(" 2. Ver todos los clientes");
                Console.WriteLine(" 3. Consultar cliente por codigo");
                Console.WriteLine(" 4. Actualizar cliente");
                Console.WriteLine(" 5. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    string codigo = LeerTexto("Codigo: ");
                    string nombre = LeerTexto("Nombre completo: ");
                    string telefono = LeerTexto("Telefono (8 digitos): ");
                    string correo = LeerTexto("Correo: ");
                    string tipo = ElegirOpcion("Tipo de cliente:", new string[] { "Estudiante", "Profesional", "Negocio", "Institucion" });
                    if (sistema.RegistrarCliente(new Cliente(codigo, nombre, telefono, correo, tipo)) == true)
                    {
                        Console.WriteLine("Cliente registrado correctamente.");
                    }
                    Pausa();
                }
                else if (opcion == 2)
                {
                    if (sistema.Clientes.Count == 0)
                    {
                        Console.WriteLine("No hay clientes registrados.");
                    }
                    for (int i = 0; i < sistema.Clientes.Count; i++)
                    {
                        sistema.Clientes[i].MostrarDatos();
                    }
                    Pausa();
                }
                else if (opcion == 3)
                {
                    Cliente cliente = sistema.BuscarCliente(LeerTexto("Codigo del cliente: "));
                    if (cliente == null)
                    {
                        Console.WriteLine("No existe un cliente con ese codigo.");
                    }
                    else
                    {
                        cliente.MostrarDatos();
                        Console.WriteLine("Equipos del cliente:");
                        for (int i = 0; i < sistema.Equipos.Count; i++)
                        {
                            if (sistema.Equipos[i].Propietario == cliente)
                            {
                                sistema.Equipos[i].MostrarDatos();
                            }
                        }
                    }
                    Pausa();
                }
                else if (opcion == 4)
                {
                    Cliente cliente = sistema.BuscarCliente(LeerTexto("Codigo del cliente: "));
                    if (cliente == null)
                    {
                        Console.WriteLine("No existe un cliente con ese codigo.");
                    }
                    else
                    {
                        cliente.MostrarDatos();
                        string nombre = LeerTexto("Nuevo nombre: ");
                        string telefono = LeerTexto("Nuevo telefono: ");
                        string correo = LeerTexto("Nuevo correo: ");
                        string tipo = ElegirOpcion("Nuevo tipo:", new string[] { "Estudiante", "Profesional", "Negocio", "Institucion" });
                        if (sistema.ActualizarCliente(cliente, nombre, telefono, correo, tipo) == true)
                        {
                            Console.WriteLine("Cliente actualizado.");
                        }
                    }
                    Pausa();
                }
            }
        }

        private void MenuTecnicos()
        {
            int opcion = 0;
            while (opcion != 4)
            {
                Titulo("GESTION DE TECNICOS");
                Console.WriteLine(" 1. Registrar tecnico");
                Console.WriteLine(" 2. Ver todos los tecnicos");
                Console.WriteLine(" 3. Cambiar disponibilidad");
                Console.WriteLine(" 4. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    string codigo = LeerTexto("Codigo: ");
                    string nombre = LeerTexto("Nombre completo: ");
                    string telefono = LeerTexto("Telefono (8 digitos): ");
                    string especialidad = ElegirOpcion("Especialidad:", new string[] { "Portatil", "Escritorio", "Impresora" });
                    if (sistema.RegistrarTecnico(new Tecnico(codigo, nombre, telefono, especialidad)) == true)
                    {
                        Console.WriteLine("Tecnico registrado correctamente.");
                    }
                    Pausa();
                }
                else if (opcion == 2)
                {
                    if (sistema.Tecnicos.Count == 0)
                    {
                        Console.WriteLine("No hay tecnicos registrados.");
                    }
                    for (int i = 0; i < sistema.Tecnicos.Count; i++)
                    {
                        sistema.Tecnicos[i].MostrarDatos();
                    }
                    Pausa();
                }
                else if (opcion == 3)
                {
                    Tecnico tecnico = sistema.BuscarTecnico(LeerTexto("Codigo del tecnico: "));
                    if (tecnico == null)
                    {
                        Console.WriteLine("No existe un tecnico con ese codigo.");
                    }
                    else
                    {
                        tecnico.Disponible = !tecnico.Disponible;
                        tecnico.MostrarDatos();
                    }
                    Pausa();
                }
            }
        }

        private void MenuEquipos()
        {
            int opcion = 0;
            while (opcion != 3)
            {
                Titulo("GESTION DE EQUIPOS");
                Console.WriteLine(" 1. Registrar equipo");
                Console.WriteLine(" 2. Ver todos los equipos");
                Console.WriteLine(" 3. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    Cliente cliente = sistema.BuscarCliente(LeerTexto("Codigo del cliente propietario: "));
                    if (cliente == null)
                    {
                        Console.WriteLine("No existe un cliente con ese codigo. Primero registre al cliente.");
                        Pausa();
                        continue;
                    }

                    string tipo = ElegirOpcion("Tipo de equipo:", new string[] { "Portatil", "Escritorio", "Impresora" });
                    string codigo = LeerTexto("Codigo interno: ");
                    string serie = LeerTexto("Numero de serie: ");
                    string marca = LeerTexto("Marca: ");
                    string modelo = LeerTexto("Modelo: ");
                    string estadoFisico = LeerTexto("Estado fisico: ");
                    string accesorios = LeerTexto("Accesorios entregados: ");

                    Equipo equipo;
                    if (tipo == "Portatil")
                    {
                        equipo = new Portatil(codigo, serie, marca, modelo, cliente, estadoFisico, accesorios, LeerSiNo("Incluye cargador"));
                    }
                    else if (tipo == "Escritorio")
                    {
                        equipo = new Escritorio(codigo, serie, marca, modelo, cliente, estadoFisico, accesorios, LeerSiNo("Incluye monitor"));
                    }
                    else
                    {
                        string impresion = ElegirOpcion("Tipo de impresion:", new string[] { "Tinta", "Laser", "Matricial" });
                        equipo = new Impresora(codigo, serie, marca, modelo, cliente, estadoFisico, accesorios, impresion);
                    }

                    if (sistema.RegistrarEquipo(equipo) == true)
                    {
                        Console.WriteLine($"Equipo registrado. Costo de revision: Q{equipo.CostoRevision():0.00}");
                    }
                    Pausa();
                }
                else if (opcion == 2)
                {
                    if (sistema.Equipos.Count == 0)
                    {
                        Console.WriteLine("No hay equipos registrados.");
                    }
                    for (int i = 0; i < sistema.Equipos.Count; i++)
                    {
                        sistema.Equipos[i].MostrarDatos();
                    }
                    Pausa();
                }
            }
        }

        private Solicitud PedirSolicitud()
        {
            Solicitud solicitud = sistema.BuscarSolicitud(LeerTexto("Codigo de la solicitud (ej. SOL-0001): ").ToUpper());
            if (solicitud == null)
            {
                Console.WriteLine("No existe una solicitud con ese codigo.");
            }
            return solicitud;
        }

        private void MostrarDetalle(Solicitud solicitud)
        {
            solicitud.MostrarDatos();
            Console.WriteLine($"   Problema: {solicitud.Problema}");
            if (solicitud.Diagnostico != "")
            {
                Console.WriteLine($"   Diagnostico: {solicitud.Diagnostico}");
            }
            Console.WriteLine($"   Revision: Q{solicitud.Equipo.CostoRevision():0.00} | Mano de obra: Q{solicitud.ManoObra:0.00}");
            if (solicitud.Repuestos.Count > 0)
            {
                Console.WriteLine("   Repuestos usados:");
                for (int i = 0; i < solicitud.Repuestos.Count; i++)
                {
                    solicitud.Repuestos[i].MostrarDatos();
                }
            }
        }

        private void MenuSolicitudes()
        {
            int opcion = 0;
            while (opcion != 10)
            {
                Titulo("GESTION DE SOLICITUDES");
                Console.WriteLine(" 1. Crear solicitud");
                Console.WriteLine(" 2. Ver todas las solicitudes");
                Console.WriteLine(" 3. Ver detalle de una solicitud");
                Console.WriteLine(" 4. Avanzar al siguiente estado");
                Console.WriteLine(" 5. Cancelar solicitud");
                Console.WriteLine(" 6. Registrar diagnostico y mano de obra");
                Console.WriteLine(" 7. Agregar repuesto");
                Console.WriteLine(" 8. Ver historial / deshacer ultimo cambio");
                Console.WriteLine(" 9. Calificar tecnico");
                Console.WriteLine("10. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    string cliente = LeerTexto("Codigo del cliente: ");
                    string equipo = LeerTexto("Codigo del equipo: ");
                    string problema = LeerTexto("Problema reportado: ");
                    string prioridad = ElegirOpcion("Prioridad:", new string[] { "Normal", "Urgente" });
                    Solicitud solicitud = sistema.CrearSolicitud(cliente, equipo, problema, prioridad);
                    if (solicitud != null)
                    {
                        Console.WriteLine($"Solicitud {solicitud.Codigo} creada y colocada en la cola {prioridad.ToLower()}.");
                    }
                    Pausa();
                }
                else if (opcion == 2)
                {
                    if (sistema.Solicitudes.Count == 0)
                    {
                        Console.WriteLine("No hay solicitudes registradas.");
                    }
                    for (int i = 0; i < sistema.Solicitudes.Count; i++)
                    {
                        sistema.Solicitudes[i].MostrarDatos();
                    }
                    Pausa();
                }
                else if (opcion == 3)
                {
                    Solicitud solicitud = PedirSolicitud();
                    if (solicitud != null)
                    {
                        MostrarDetalle(solicitud);
                    }
                    Pausa();
                }
                else if (opcion == 4)
                {
                    Solicitud solicitud = PedirSolicitud();
                    if (solicitud != null)
                    {
                        if (solicitud.EstaActiva() == false)
                        {
                            Console.WriteLine("La solicitud ya fue entregada o cancelada.");
                        }
                        else
                        {
                            EstadoSolicitud siguiente = solicitud.Estado + 1;
                            if (sistema.CambiarEstado(solicitud, siguiente) == true)
                            {
                                Console.WriteLine($"La solicitud {solicitud.Codigo} ahora esta en {solicitud.Estado}.");
                                if (solicitud.Estado == EstadoSolicitud.Entregada)
                                {
                                    Console.WriteLine($"Total a pagar: Q{solicitud.CalcularTotal():0.00}");
                                }
                            }
                        }
                    }
                    Pausa();
                }
                else if (opcion == 5)
                {
                    Solicitud solicitud = PedirSolicitud();
                    if (solicitud != null && LeerSiNo($"Seguro que desea cancelar {solicitud.Codigo}") == true)
                    {
                        if (sistema.CambiarEstado(solicitud, EstadoSolicitud.Cancelada) == true)
                        {
                            Console.WriteLine("Solicitud cancelada.");
                        }
                    }
                    Pausa();
                }
                else if (opcion == 6)
                {
                    Solicitud solicitud = PedirSolicitud();
                    if (solicitud != null)
                    {
                        string diagnostico = LeerTexto("Diagnostico: ");
                        double manoObra = LeerDouble("Costo de mano de obra: Q");
                        if (sistema.RegistrarDiagnostico(solicitud, diagnostico, manoObra) == true)
                        {
                            Console.WriteLine("Diagnostico registrado.");
                        }
                    }
                    Pausa();
                }
                else if (opcion == 7)
                {
                    Solicitud solicitud = PedirSolicitud();
                    if (solicitud != null)
                    {
                        for (int i = 0; i < sistema.Repuestos.Count; i++)
                        {
                            sistema.Repuestos[i].MostrarDatos();
                        }
                        string codigo = LeerTexto("Codigo del repuesto: ");
                        int cantidad = LeerEntero("Cantidad: ");
                        if (sistema.UsarRepuesto(solicitud, codigo, cantidad) == true)
                        {
                            Console.WriteLine($"Repuesto agregado. Nuevo total: Q{solicitud.CalcularTotal():0.00}");
                        }
                    }
                    Pausa();
                }
                else if (opcion == 8)
                {
                    Solicitud solicitud = PedirSolicitud();
                    if (solicitud != null)
                    {
                        Console.WriteLine($"Estado actual: {solicitud.Estado}");
                        solicitud.MostrarHistorial();
                        if (solicitud.Historial.Count > 0 && LeerSiNo("Desea deshacer el ultimo cambio") == true)
                        {
                            if (sistema.DeshacerCambio(solicitud) == true)
                            {
                                Console.WriteLine($"Cambio deshecho. La solicitud regreso a {solicitud.Estado}.");
                            }
                        }
                    }
                    Pausa();
                }
                else if (opcion == 9)
                {
                    Solicitud solicitud = PedirSolicitud();
                    if (solicitud != null)
                    {
                        int nota = LeerEntero("Calificacion del 1 al 5: ");
                        if (sistema.CalificarTecnico(solicitud, nota) == true)
                        {
                            Console.WriteLine("Gracias por calificar al tecnico.");
                        }
                    }
                    Pausa();
                }
            }
        }

        private void MenuRepuestos()
        {
            int opcion = 0;
            while (opcion != 4)
            {
                Titulo("GESTION DE REPUESTOS");
                Console.WriteLine(" 1. Registrar repuesto");
                Console.WriteLine(" 2. Ver todos los repuestos");
                Console.WriteLine(" 3. Agregar existencia");
                Console.WriteLine(" 4. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    string codigo = LeerTexto("Codigo: ");
                    string nombre = LeerTexto("Nombre: ");
                    string categoria = LeerTexto("Categoria: ");
                    int existencia = LeerEntero("Existencia: ");
                    int minima = LeerEntero("Existencia minima: ");
                    double costo = LeerDouble("Costo unitario: Q");
                    double precio = LeerDouble("Precio de venta: Q");
                    if (sistema.RegistrarRepuesto(new Repuesto(codigo, nombre, categoria, existencia, minima, costo, precio)) == true)
                    {
                        Console.WriteLine("Repuesto registrado correctamente.");
                    }
                    Pausa();
                }
                else if (opcion == 2)
                {
                    if (sistema.Repuestos.Count == 0)
                    {
                        Console.WriteLine("No hay repuestos registrados.");
                    }
                    for (int i = 0; i < sistema.Repuestos.Count; i++)
                    {
                        sistema.Repuestos[i].MostrarDatos();
                    }
                    Pausa();
                }
                else if (opcion == 3)
                {
                    Repuesto repuesto = sistema.BuscarRepuesto(LeerTexto("Codigo del repuesto: "));
                    if (repuesto == null)
                    {
                        Console.WriteLine("No existe un repuesto con ese codigo.");
                    }
                    else
                    {
                        int cantidad = LeerEntero("Cantidad que ingresa: ");
                        if (cantidad <= 0)
                        {
                            Console.WriteLine("La cantidad debe ser mayor que cero.");
                        }
                        else
                        {
                            repuesto.Existencia = repuesto.Existencia + cantidad;
                            repuesto.MostrarDatos();
                        }
                    }
                    Pausa();
                }
            }
        }

        private void MostrarCola(string nombre, Queue<Solicitud> cola)
        {
            Console.WriteLine($"Cola {nombre} ({cola.Count}):");
            if (cola.Count == 0)
            {
                Console.WriteLine("   (vacia)");
            }
            foreach (Solicitud solicitud in cola)
            {
                if (solicitud.Estado == EstadoSolicitud.EnEspera)
                {
                    Console.WriteLine($"   {solicitud.Codigo} | {solicitud.Cliente.Nombre} | {solicitud.Equipo.Tipo} | {solicitud.Problema}");
                }
            }
        }

        private void MenuColas()
        {
            int opcion = 0;
            while (opcion != 3)
            {
                Titulo("ATENCION DE COLAS");
                Console.WriteLine(" 1. Ver colas");
                Console.WriteLine(" 2. Atender siguiente solicitud");
                Console.WriteLine(" 3. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    MostrarCola("urgente", sistema.ColaUrgente);
                    MostrarCola("normal", sistema.ColaNormal);
                    Pausa();
                }
                else if (opcion == 2)
                {
                    Solicitud solicitud = sistema.AtenderSiguiente();
                    if (solicitud != null)
                    {
                        Console.WriteLine($"Se atendio {solicitud.Codigo} ({solicitud.Prioridad}) y se asigno a {solicitud.Tecnico.Nombre}.");
                    }
                    Pausa();
                }
            }
        }

        private void EncabezadoOrden(string titulo, string algoritmo, string criterio, bool ascendente)
        {
            string orden = "Descendente";
            if (ascendente == true)
            {
                orden = "Ascendente";
            }
            Console.WriteLine();
            Console.WriteLine("==================================================================");
            Console.WriteLine(" " + titulo);
            Console.WriteLine($" Algoritmo: {algoritmo} | Criterio: {criterio} | Orden: {orden}");
            Console.WriteLine("==================================================================");
        }

        private void FilaSolicitud(Solicitud s)
        {
            Console.WriteLine($" {s.Codigo} | {s.Cliente.Nombre} | {s.Equipo.Tipo} | {s.Estado} | {s.FechaRecepcion:dd/MM/yyyy} | Q{s.CalcularTotal():0.00}");
        }

        private void EncabezadoSolicitudes()
        {
            Console.WriteLine(" Codigo | Cliente | Equipo | Estado | Fecha | Total");
            Console.WriteLine("------------------------------------------------------------------------------");
        }

        private bool HayDatos(int cantidad)
        {
            if (cantidad < 2)
            {
                Console.WriteLine("No hay suficientes datos para ordenar (se necesitan por lo menos 2).");
                return false;
            }
            return true;
        }

        private void OrdenarSolicitudesPorTotal(string titulo, bool ascendente)
        {
            if (HayDatos(sistema.Solicitudes.Count) == false)
            {
                return;
            }
            List<Solicitud> datos = Ordenamientos.BubblePorTotal(sistema.Solicitudes, ascendente);
            EncabezadoOrden(titulo, "Bubble Sort", "Total (Q)", ascendente);
            EncabezadoSolicitudes();
            for (int i = 0; i < datos.Count; i++)
            {
                FilaSolicitud(datos[i]);
            }
            Console.WriteLine($"\n Registros: {datos.Count}     Intercambios realizados: {Ordenamientos.Intercambios}");
        }

        private void OrdenarSolicitudesPorFecha(string titulo, bool ascendente)
        {
            if (HayDatos(sistema.Solicitudes.Count) == false)
            {
                return;
            }
            List<Solicitud> datos = Ordenamientos.QuickPorFecha(sistema.Solicitudes, ascendente);
            EncabezadoOrden(titulo, "Quick Sort", "Fecha de recepcion", ascendente);
            EncabezadoSolicitudes();
            for (int i = 0; i < datos.Count; i++)
            {
                FilaSolicitud(datos[i]);
            }
            Console.WriteLine($"\n Registros: {datos.Count}     Intercambios realizados: {Ordenamientos.Intercambios}");
        }

        private void OrdenarTecnicos(string titulo, bool ascendente)
        {
            if (HayDatos(sistema.Tecnicos.Count) == false)
            {
                return;
            }
            List<Tecnico> datos = Ordenamientos.SelectionPorServicios(sistema.Tecnicos, ascendente);
            EncabezadoOrden(titulo, "Selection Sort", "Servicios finalizados", ascendente);
            Console.WriteLine(" Codigo | Nombre | Especialidad | Finalizados | Promedio");
            Console.WriteLine("------------------------------------------------------------------");
            for (int i = 0; i < datos.Count; i++)
            {
                Console.WriteLine($" {datos[i].Codigo} | {datos[i].Nombre} | {datos[i].Especialidad} | {datos[i].ServiciosFinalizados} | {datos[i].Promedio():0.0}");
            }
            Console.WriteLine($"\n Registros: {datos.Count}     Intercambios realizados: {Ordenamientos.Intercambios}");
        }

        private void OrdenarRepuestos(string titulo, bool ascendente)
        {
            if (HayDatos(sistema.Repuestos.Count) == false)
            {
                return;
            }
            List<Repuesto> datos = Ordenamientos.ShellPorExistencia(sistema.Repuestos, ascendente);
            EncabezadoOrden(titulo, "Shell Sort", "Existencia", ascendente);
            Console.WriteLine(" Codigo | Nombre | Categoria | Existencia | Minima");
            Console.WriteLine("------------------------------------------------------------------");
            for (int i = 0; i < datos.Count; i++)
            {
                Console.WriteLine($" {datos[i].Codigo} | {datos[i].Nombre} | {datos[i].Categoria} | {datos[i].Existencia} | {datos[i].ExistenciaMinima}");
            }
            Console.WriteLine($"\n Registros: {datos.Count}     Movimientos realizados: {Ordenamientos.Intercambios}");
        }

        private void MenuOrdenamientos()
        {
            int opcion = 0;
            while (opcion != 5)
            {
                Titulo("ORDENAMIENTOS");
                Console.WriteLine(" 1. Solicitudes por costo total (Bubble Sort)");
                Console.WriteLine(" 2. Tecnicos por servicios finalizados (Selection Sort)");
                Console.WriteLine(" 3. Repuestos por existencia (Shell Sort)");
                Console.WriteLine(" 4. Solicitudes por fecha de recepcion (Quick Sort)");
                Console.WriteLine(" 5. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion >= 1 && opcion <= 4)
                {
                    bool ascendente = LeerOrdenAscendente();
                    if (opcion == 1)
                    {
                        OrdenarSolicitudesPorTotal("SOLICITUDES ORDENADAS POR COSTO TOTAL", ascendente);
                    }
                    else if (opcion == 2)
                    {
                        OrdenarTecnicos("TECNICOS ORDENADOS POR SERVICIOS FINALIZADOS", ascendente);
                    }
                    else if (opcion == 3)
                    {
                        OrdenarRepuestos("REPUESTOS ORDENADOS POR EXISTENCIA", ascendente);
                    }
                    else
                    {
                        OrdenarSolicitudesPorFecha("SOLICITUDES ORDENADAS POR FECHA", ascendente);
                    }
                    Pausa();
                }
            }
        }

        private void GuardarResultado(string busqueda, int encontrados, int comparaciones)
        {
            resultadosBusquedas.Add($"{DateTime.Now:HH:mm} | {busqueda} | Encontrados: {encontrados} | Comparaciones: {comparaciones}");
        }

        private void MenuBusquedas()
        {
            int opcion = 0;
            while (opcion != 5)
            {
                Titulo("BUSQUEDAS");
                Console.WriteLine(" 1. Clientes por nombre (secuencial)");
                Console.WriteLine(" 2. Solicitudes por estado (secuencial)");
                Console.WriteLine(" 3. Equipos por marca (secuencial)");
                Console.WriteLine(" 4. Solicitud por codigo (binaria)");
                Console.WriteLine(" 5. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    string texto = LeerTexto("Nombre o parte del nombre: ");
                    List<Cliente> encontrados = Busquedas.ClientesPorNombre(sistema.Clientes, texto);
                    if (encontrados.Count == 0)
                    {
                        Console.WriteLine("No se encontraron clientes.");
                    }
                    for (int i = 0; i < encontrados.Count; i++)
                    {
                        encontrados[i].MostrarDatos();
                    }
                    Console.WriteLine($"Comparaciones: {Busquedas.Comparaciones}");
                    GuardarResultado($"Clientes con nombre '{texto}'", encontrados.Count, Busquedas.Comparaciones);
                    Pausa();
                }
                else if (opcion == 2)
                {
                    string[] nombres = Enum.GetNames(typeof(EstadoSolicitud));
                    string elegido = ElegirOpcion("Estado:", nombres);
                    EstadoSolicitud estado = (EstadoSolicitud)Enum.Parse(typeof(EstadoSolicitud), elegido);
                    List<Solicitud> encontradas = Busquedas.SolicitudesPorEstado(sistema.Solicitudes, estado);
                    if (encontradas.Count == 0)
                    {
                        Console.WriteLine("No hay resultados.");
                    }
                    for (int i = 0; i < encontradas.Count; i++)
                    {
                        encontradas[i].MostrarDatos();
                    }
                    Console.WriteLine($"Comparaciones: {Busquedas.Comparaciones}");
                    GuardarResultado($"Solicitudes en estado {estado}", encontradas.Count, Busquedas.Comparaciones);
                    Pausa();
                }
                else if (opcion == 3)
                {
                    string marca = LeerTexto("Marca: ");
                    List<Equipo> encontrados = Busquedas.EquiposPorMarca(sistema.Equipos, marca);
                    if (encontrados.Count == 0)
                    {
                        Console.WriteLine("No hay resultados.");
                    }
                    for (int i = 0; i < encontrados.Count; i++)
                    {
                        encontrados[i].MostrarDatos();
                    }
                    Console.WriteLine($"Comparaciones: {Busquedas.Comparaciones}");
                    GuardarResultado($"Equipos de marca {marca}", encontrados.Count, Busquedas.Comparaciones);
                    Pausa();
                }
                else if (opcion == 4)
                {
                    if (sistema.Solicitudes.Count == 0)
                    {
                        Console.WriteLine("No hay solicitudes registradas.");
                        Pausa();
                        continue;
                    }
                    string codigo = LeerTexto("Codigo de la solicitud (ej. SOL-0001): ").ToUpper();
                    Solicitud[] ordenadas = Busquedas.OrdenarPorCodigo(sistema.Solicitudes);
                    Solicitud solicitud = Busquedas.BinariaPorCodigo(ordenadas, codigo);
                    int encontrados = 0;
                    if (solicitud == null)
                    {
                        Console.WriteLine("No existe una solicitud con ese codigo.");
                    }
                    else
                    {
                        MostrarDetalle(solicitud);
                        encontrados = 1;
                    }
                    Console.WriteLine($"Comparaciones: {Busquedas.Comparaciones}");
                    GuardarResultado($"Solicitud {codigo} (binaria)", encontrados, Busquedas.Comparaciones);
                    Pausa();
                }
            }
        }

        public void MostrarReportes()
        {
            int opcion = 0;
            while (opcion != 13)
            {
                Titulo("REPORTES");
                Console.WriteLine(" 1. Solicitudes en espera por prioridad");
                Console.WriteLine(" 2. Solicitudes activas, finalizadas y canceladas");
                Console.WriteLine(" 3. Tecnicos disponibles y con carga completa");
                Console.WriteLine(" 4. Tecnico con mas servicios (Selection Sort)");
                Console.WriteLine(" 5. Equipos atendidos por tipo");
                Console.WriteLine(" 6. Solicitudes por costo total (Bubble Sort)");
                Console.WriteLine(" 7. Solicitudes por fecha (Quick Sort)");
                Console.WriteLine(" 8. Repuestos por existencia (Shell Sort)");
                Console.WriteLine(" 9. Repuestos en minimo o agotados");
                Console.WriteLine("10. Ingresos por servicios finalizados");
                Console.WriteLine("11. Resultados de las busquedas");
                Console.WriteLine("12. Matriz semanal de carga");
                Console.WriteLine("13. Regresar");
                opcion = LeerEntero("Opcion: ");

                if (opcion == 1)
                {
                    Titulo("REPORTE 1: SOLICITUDES EN ESPERA POR PRIORIDAD");
                    MostrarCola("urgente", sistema.ColaUrgente);
                    MostrarCola("normal", sistema.ColaNormal);
                }
                else if (opcion == 2)
                {
                    ReporteSolicitudesPorGrupo();
                }
                else if (opcion == 3)
                {
                    ReporteTecnicos();
                }
                else if (opcion == 4)
                {
                    OrdenarTecnicos("REPORTE 4: TECNICOS POR SERVICIOS FINALIZADOS", false);
                    if (sistema.Tecnicos.Count >= 2)
                    {
                        Tecnico mejor = Ordenamientos.SelectionPorServicios(sistema.Tecnicos, false)[0];
                        Console.WriteLine($" Tecnico con mas servicios: {mejor.Nombre} ({mejor.ServiciosFinalizados})");
                    }
                }
                else if (opcion == 5)
                {
                    ReporteEquiposPorTipo();
                }
                else if (opcion == 6)
                {
                    OrdenarSolicitudesPorTotal("REPORTE 6: SOLICITUDES ORDENADAS POR COSTO TOTAL", false);
                }
                else if (opcion == 7)
                {
                    OrdenarSolicitudesPorFecha("REPORTE 7: SOLICITUDES ORDENADAS POR FECHA", true);
                }
                else if (opcion == 8)
                {
                    OrdenarRepuestos("REPORTE 8: REPUESTOS ORDENADOS POR EXISTENCIA", true);
                }
                else if (opcion == 9)
                {
                    ReporteRepuestosEnMinimo();
                }
                else if (opcion == 10)
                {
                    ReporteIngresos();
                }
                else if (opcion == 11)
                {
                    Titulo("REPORTE 11: RESULTADOS DE LAS BUSQUEDAS");
                    if (resultadosBusquedas.Count == 0)
                    {
                        Console.WriteLine("Todavia no se ha hecho ninguna busqueda.");
                    }
                    for (int i = 0; i < resultadosBusquedas.Count; i++)
                    {
                        Console.WriteLine(" " + resultadosBusquedas[i]);
                    }
                }
                else if (opcion == 12)
                {
                    Titulo("REPORTE 12: MATRIZ SEMANAL DE CARGA");
                    sistema.MostrarCargaSemanal();
                }

                if (opcion >= 1 && opcion <= 12)
                {
                    Pausa();
                }
            }
        }

        private void ReporteSolicitudesPorGrupo()
        {
            Titulo("REPORTE 2: SOLICITUDES ACTIVAS, FINALIZADAS Y CANCELADAS");
            string[] grupos = { "ACTIVAS", "FINALIZADAS", "CANCELADAS" };
            for (int g = 0; g < 3; g++)
            {
                Console.WriteLine();
                Console.WriteLine(grupos[g] + ":");
                int cantidad = 0;
                for (int i = 0; i < sistema.Solicitudes.Count; i++)
                {
                    Solicitud s = sistema.Solicitudes[i];
                    bool entra = false;
                    if (g == 0 && s.EstaActiva() == true)
                    {
                        entra = true;
                    }
                    else if (g == 1 && s.Estado == EstadoSolicitud.Entregada)
                    {
                        entra = true;
                    }
                    else if (g == 2 && s.Estado == EstadoSolicitud.Cancelada)
                    {
                        entra = true;
                    }

                    if (entra == true)
                    {
                        FilaSolicitud(s);
                        cantidad = cantidad + 1;
                    }
                }
                Console.WriteLine($" Total: {cantidad}");
            }
        }

        private void ReporteTecnicos()
        {
            Titulo("REPORTE 3: TECNICOS DISPONIBLES Y CON CARGA COMPLETA");
            Console.WriteLine("Disponibles (pueden recibir trabajo):");
            for (int i = 0; i < sistema.Tecnicos.Count; i++)
            {
                Tecnico t = sistema.Tecnicos[i];
                if (t.Disponible == true && t.TrabajosActivos < 3)
                {
                    Console.WriteLine($"   {t.Codigo} | {t.Nombre} | {t.Especialidad} | Activos: {t.TrabajosActivos}");
                }
            }
            Console.WriteLine("Con carga completa o no disponibles:");
            for (int i = 0; i < sistema.Tecnicos.Count; i++)
            {
                Tecnico t = sistema.Tecnicos[i];
                if (t.Disponible == false || t.TrabajosActivos >= 3)
                {
                    Console.WriteLine($"   {t.Codigo} | {t.Nombre} | {t.Especialidad} | Activos: {t.TrabajosActivos}");
                }
            }
        }

        private void ReporteEquiposPorTipo()
        {
            Titulo("REPORTE 5: EQUIPOS ATENDIDOS POR TIPO");
            string[] tipos = { "Portatil", "Escritorio", "Impresora" };
            int[] conteo = new int[3];
            for (int i = 0; i < sistema.Solicitudes.Count; i++)
            {
                for (int t = 0; t < tipos.Length; t++)
                {
                    if (sistema.Solicitudes[i].Equipo.Tipo == tipos[t])
                    {
                        conteo[t] = conteo[t] + 1;
                    }
                }
            }
            for (int t = 0; t < tipos.Length; t++)
            {
                Console.WriteLine($" {tipos[t]} | {conteo[t]} solicitudes");
            }
        }

        private void ReporteRepuestosEnMinimo()
        {
            Titulo("REPORTE 9: REPUESTOS EN MINIMO O AGOTADOS");
            int cantidad = 0;
            for (int i = 0; i < sistema.Repuestos.Count; i++)
            {
                Repuesto r = sistema.Repuestos[i];
                if (r.EstaEnMinimo() == true)
                {
                    string estado = "En minimo";
                    if (r.Existencia == 0)
                    {
                        estado = "AGOTADO";
                    }
                    Console.WriteLine($" {r.Codigo} | {r.Nombre} | Existencia: {r.Existencia} | Minima: {r.ExistenciaMinima} | {estado}");
                    cantidad = cantidad + 1;
                }
            }
            if (cantidad == 0)
            {
                Console.WriteLine("Ningun repuesto esta en su minimo.");
            }
        }

        private void ReporteIngresos()
        {
            Titulo("REPORTE 10: INGRESOS POR SERVICIOS FINALIZADOS");
            double total = 0;
            int cantidad = 0;
            for (int i = 0; i < sistema.Solicitudes.Count; i++)
            {
                Solicitud s = sistema.Solicitudes[i];
                if (s.Estado == EstadoSolicitud.Entregada)
                {
                    Console.WriteLine($" {s.Codigo} | {s.Cliente.Nombre} | Q{s.CalcularTotal():0.00}");
                    total = total + s.CalcularTotal();
                    cantidad = cantidad + 1;
                }
            }
            Console.WriteLine($" Servicios finalizados: {cantidad}");
            Console.WriteLine($" Ingresos totales: Q{total:0.00}");
        }
    }
}
