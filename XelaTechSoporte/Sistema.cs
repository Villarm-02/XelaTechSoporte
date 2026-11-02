using System;
using System.Collections.Generic;

namespace XelaTechSoporte
{
    public class Sistema
    {
        private List<Cliente> clientes = new List<Cliente>();
        private List<Tecnico> tecnicos = new List<Tecnico>();
        private List<Equipo> equipos = new List<Equipo>();
        private List<Solicitud> solicitudes = new List<Solicitud>();
        private List<Repuesto> repuestos = new List<Repuesto>();

        private Queue<Solicitud> colaUrgente = new Queue<Solicitud>();
        private Queue<Solicitud> colaNormal = new Queue<Solicitud>();

        private int[,] cargaSemanal = new int[10, 6];
        private string[] dias = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };

        private int contadorSolicitudes = 0;

        public List<Cliente> Clientes
        {
            get { return clientes; }
        }

        public List<Tecnico> Tecnicos
        {
            get { return tecnicos; }
        }

        public List<Equipo> Equipos
        {
            get { return equipos; }
        }

        public List<Solicitud> Solicitudes
        {
            get { return solicitudes; }
        }

        public List<Repuesto> Repuestos
        {
            get { return repuestos; }
        }

        public Queue<Solicitud> ColaUrgente
        {
            get { return colaUrgente; }
        }

        public Queue<Solicitud> ColaNormal
        {
            get { return colaNormal; }
        }

        public int[,] CargaSemanal
        {
            get { return cargaSemanal; }
        }

        private bool EstaVacio(string texto)
        {
            if (texto == null || texto.Trim() == "")
            {
                return true;
            }
            return false;
        }

        private bool TelefonoValido(string telefono)
        {
            if (telefono == null || telefono.Length != 8)
            {
                return false;
            }

            for (int i = 0; i < telefono.Length; i++)
            {
                if (telefono[i] < '0' || telefono[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private bool CorreoValido(string correo)
        {
            if (correo == null)
            {
                return false;
            }

            int arrobas = 0;
            for (int i = 0; i < correo.Length; i++)
            {
                if (correo[i] == '@')
                {
                    arrobas = arrobas + 1;
                }
            }

            if (arrobas != 1 || correo[0] == '@' || correo[correo.Length - 1] == '@')
            {
                return false;
            }
            return true;
        }

        public Cliente BuscarCliente(string codigo)
        {
            for (int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].Codigo == codigo)
                {
                    return clientes[i];
                }
            }
            return null;
        }

        public Tecnico BuscarTecnico(string codigo)
        {
            for (int i = 0; i < tecnicos.Count; i++)
            {
                if (tecnicos[i].Codigo == codigo)
                {
                    return tecnicos[i];
                }
            }
            return null;
        }

        public Equipo BuscarEquipo(string codigo)
        {
            for (int i = 0; i < equipos.Count; i++)
            {
                if (equipos[i].Codigo == codigo)
                {
                    return equipos[i];
                }
            }
            return null;
        }

        public Repuesto BuscarRepuesto(string codigo)
        {
            for (int i = 0; i < repuestos.Count; i++)
            {
                if (repuestos[i].Codigo == codigo)
                {
                    return repuestos[i];
                }
            }
            return null;
        }

        public Solicitud BuscarSolicitud(string codigo)
        {
            for (int i = 0; i < solicitudes.Count; i++)
            {
                if (solicitudes[i].Codigo == codigo)
                {
                    return solicitudes[i];
                }
            }
            return null;
        }

        public bool RegistrarCliente(Cliente cliente)
        {
            if (EstaVacio(cliente.Codigo) || EstaVacio(cliente.Nombre) || EstaVacio(cliente.Tipo))
            {
                Console.WriteLine("El codigo, el nombre y el tipo no pueden estar vacios.");
                return false;
            }

            if (BuscarCliente(cliente.Codigo) != null)
            {
                Console.WriteLine($"Ya existe un cliente con el codigo {cliente.Codigo}.");
                return false;
            }

            if (TelefonoValido(cliente.Telefono) == false)
            {
                Console.WriteLine("El telefono debe tener 8 digitos.");
                return false;
            }

            if (CorreoValido(cliente.Correo) == false)
            {
                Console.WriteLine("El correo debe tener una arroba (@).");
                return false;
            }

            clientes.Add(cliente);
            return true;
        }

        public bool ActualizarCliente(Cliente cliente, string nombre, string telefono, string correo, string tipo)
        {
            if (EstaVacio(nombre) || EstaVacio(tipo))
            {
                Console.WriteLine("El nombre y el tipo no pueden estar vacios.");
                return false;
            }

            if (TelefonoValido(telefono) == false)
            {
                Console.WriteLine("El telefono debe tener 8 digitos.");
                return false;
            }

            if (CorreoValido(correo) == false)
            {
                Console.WriteLine("El correo debe tener una arroba (@).");
                return false;
            }

            cliente.Nombre = nombre;
            cliente.Telefono = telefono;
            cliente.Correo = correo;
            cliente.Tipo = tipo;
            return true;
        }

        public bool RegistrarTecnico(Tecnico tecnico)
        {
            if (tecnicos.Count >= 10)
            {
                Console.WriteLine("No se pueden registrar mas de 10 tecnicos.");
                return false;
            }

            if (EstaVacio(tecnico.Codigo) || EstaVacio(tecnico.Nombre) || EstaVacio(tecnico.Especialidad))
            {
                Console.WriteLine("El codigo, el nombre y la especialidad no pueden estar vacios.");
                return false;
            }

            if (BuscarTecnico(tecnico.Codigo) != null)
            {
                Console.WriteLine($"Ya existe un tecnico con el codigo {tecnico.Codigo}.");
                return false;
            }

            if (TelefonoValido(tecnico.Telefono) == false)
            {
                Console.WriteLine("El telefono debe tener 8 digitos.");
                return false;
            }

            tecnicos.Add(tecnico);
            return true;
        }

        public bool RegistrarEquipo(Equipo equipo)
        {
            if (EstaVacio(equipo.Codigo) || EstaVacio(equipo.Serie) || EstaVacio(equipo.Marca) || EstaVacio(equipo.Modelo))
            {
                Console.WriteLine("El codigo, la serie, la marca y el modelo no pueden estar vacios.");
                return false;
            }

            if (equipo.Propietario == null || BuscarCliente(equipo.Propietario.Codigo) == null)
            {
                Console.WriteLine("El propietario del equipo no existe.");
                return false;
            }

            if (BuscarEquipo(equipo.Codigo) != null)
            {
                Console.WriteLine($"Ya existe un equipo con el codigo {equipo.Codigo}.");
                return false;
            }

            for (int i = 0; i < equipos.Count; i++)
            {
                if (equipos[i].Serie == equipo.Serie)
                {
                    Console.WriteLine($"Ya existe un equipo con la serie {equipo.Serie}.");
                    return false;
                }
            }

            equipos.Add(equipo);
            return true;
        }

        public bool RegistrarRepuesto(Repuesto repuesto)
        {
            if (EstaVacio(repuesto.Codigo) || EstaVacio(repuesto.Nombre) || EstaVacio(repuesto.Categoria))
            {
                Console.WriteLine("El codigo, el nombre y la categoria no pueden estar vacios.");
                return false;
            }

            if (BuscarRepuesto(repuesto.Codigo) != null)
            {
                Console.WriteLine($"Ya existe un repuesto con el codigo {repuesto.Codigo}.");
                return false;
            }

            if (repuesto.Existencia < 0 || repuesto.ExistenciaMinima < 0 || repuesto.Costo < 0 || repuesto.PrecioVenta < 0)
            {
                Console.WriteLine("La existencia, el minimo, el costo y el precio no pueden ser negativos.");
                return false;
            }

            if (repuesto.PrecioVenta < repuesto.Costo)
            {
                Console.WriteLine("El precio de venta no puede ser menor que el costo.");
                return false;
            }

            repuestos.Add(repuesto);
            return true;
        }

        public Solicitud CrearSolicitud(string codigoCliente, string codigoEquipo, string problema, string prioridad)
        {
            Cliente cliente = BuscarCliente(codigoCliente);
            if (cliente == null)
            {
                Console.WriteLine("No existe un cliente con ese codigo.");
                return null;
            }

            Equipo equipo = BuscarEquipo(codigoEquipo);
            if (equipo == null)
            {
                Console.WriteLine("No existe un equipo con ese codigo.");
                return null;
            }

            if (equipo.Propietario != cliente)
            {
                Console.WriteLine("El equipo no pertenece a ese cliente.");
                return null;
            }

            if (EstaVacio(problema))
            {
                Console.WriteLine("Debe escribir el problema del equipo.");
                return null;
            }

            for (int i = 0; i < solicitudes.Count; i++)
            {
                if (solicitudes[i].Equipo == equipo && solicitudes[i].EstaActiva() == true)
                {
                    Console.WriteLine($"El equipo ya tiene una solicitud activa ({solicitudes[i].Codigo}).");
                    return null;
                }
            }

            contadorSolicitudes = contadorSolicitudes + 1;
            string codigo = "SOL-" + contadorSolicitudes.ToString("0000");

            Solicitud solicitud;
            if (prioridad == "Urgente")
            {
                solicitud = new Solicitud(codigo, cliente, equipo, problema, "Urgente");
                colaUrgente.Enqueue(solicitud);
            }
            else
            {
                solicitud = new Solicitud(codigo, cliente, equipo, problema);
                colaNormal.Enqueue(solicitud);
            }

            solicitud.CambiarEstado(EstadoSolicitud.EnEspera);
            solicitudes.Add(solicitud);
            cliente.SumarSolicitud();
            return solicitud;
        }

        private void QuitarCanceladas(Queue<Solicitud> cola)
        {
            while (cola.Count > 0 && cola.Peek().Estado != EstadoSolicitud.EnEspera)
            {
                cola.Dequeue();
            }
        }

        private Tecnico ElegirTecnico(string tipoEquipo)
        {
            Tecnico elegido = null;
            for (int i = 0; i < tecnicos.Count; i++)
            {
                if (tecnicos[i].PuedeAtender(tipoEquipo) == true)
                {
                    if (elegido == null || tecnicos[i].TrabajosActivos < elegido.TrabajosActivos)
                    {
                        elegido = tecnicos[i];
                    }
                }
            }
            return elegido;
        }

        private int DiaDeHoy()
        {
            int dia = (int)DateTime.Now.DayOfWeek - 1;
            if (dia < 0)
            {
                dia = 5;
            }
            return dia;
        }

        public Solicitud AtenderSiguiente()
        {
            return AtenderSiguiente(DiaDeHoy());
        }

        public Solicitud AtenderSiguiente(int dia)
        {
            QuitarCanceladas(colaUrgente);
            QuitarCanceladas(colaNormal);

            Queue<Solicitud> cola;
            if (colaUrgente.Count > 0)
            {
                cola = colaUrgente;
            }
            else if (colaNormal.Count > 0)
            {
                cola = colaNormal;
            }
            else
            {
                Console.WriteLine("No hay solicitudes en espera.");
                return null;
            }

            Solicitud solicitud = cola.Peek();
            Tecnico tecnico = ElegirTecnico(solicitud.Equipo.Tipo);
            if (tecnico == null)
            {
                Console.WriteLine($"No hay un tecnico disponible de {solicitud.Equipo.Tipo} para {solicitud.Codigo}. Sigue en la cola.");
                return null;
            }

            cola.Dequeue();
            solicitud.Tecnico = tecnico;
            solicitud.CambiarEstado(EstadoSolicitud.Asignada);
            tecnico.TrabajosActivos = tecnico.TrabajosActivos + 1;

            int fila = tecnicos.IndexOf(tecnico);
            cargaSemanal[fila, dia] = cargaSemanal[fila, dia] + 1;
            return solicitud;
        }

        public bool CambiarEstado(Solicitud solicitud, EstadoSolicitud nuevoEstado)
        {
            if (nuevoEstado == EstadoSolicitud.Asignada)
            {
                Console.WriteLine("Para asignar un tecnico se usa la opcion de atender la cola.");
                return false;
            }

            if (solicitud.CambiarEstado(nuevoEstado) == false)
            {
                return false;
            }

            if (solicitud.Tecnico != null)
            {
                if (nuevoEstado == EstadoSolicitud.Entregada)
                {
                    solicitud.Tecnico.TrabajosActivos = solicitud.Tecnico.TrabajosActivos - 1;
                    solicitud.Tecnico.ServiciosFinalizados = solicitud.Tecnico.ServiciosFinalizados + 1;
                }
                else if (nuevoEstado == EstadoSolicitud.Cancelada)
                {
                    solicitud.Tecnico.TrabajosActivos = solicitud.Tecnico.TrabajosActivos - 1;
                }
            }
            return true;
        }

        public bool DeshacerCambio(Solicitud solicitud)
        {
            if (solicitud.Estado == EstadoSolicitud.Asignada)
            {
                Console.WriteLine("No se puede deshacer la asignacion del tecnico.");
                return false;
            }

            if (solicitud.Estado == EstadoSolicitud.EnEspera)
            {
                Console.WriteLine("La solicitud esta en la cola, no se puede regresar a Recibida.");
                return false;
            }

            return solicitud.DeshacerCambio();
        }

        public bool RegistrarDiagnostico(Solicitud solicitud, string diagnostico, double manoObra)
        {
            if (solicitud.Estado != EstadoSolicitud.EnDiagnostico && solicitud.Estado != EstadoSolicitud.EnReparacion)
            {
                Console.WriteLine("El diagnostico solo se registra cuando la solicitud esta en diagnostico o en reparacion.");
                return false;
            }

            if (EstaVacio(diagnostico))
            {
                Console.WriteLine("El diagnostico no puede estar vacio.");
                return false;
            }

            if (manoObra < 0)
            {
                Console.WriteLine("La mano de obra no puede ser negativa.");
                return false;
            }

            solicitud.Diagnostico = diagnostico;
            solicitud.ManoObra = manoObra;
            return true;
        }

        public bool UsarRepuesto(Solicitud solicitud, string codigoRepuesto, int cantidad)
        {
            if (solicitud.Estado != EstadoSolicitud.EnDiagnostico && solicitud.Estado != EstadoSolicitud.EnReparacion)
            {
                Console.WriteLine("Los repuestos solo se agregan en diagnostico o en reparacion.");
                return false;
            }

            Repuesto repuesto = BuscarRepuesto(codigoRepuesto);
            if (repuesto == null)
            {
                Console.WriteLine("No existe un repuesto con ese codigo.");
                return false;
            }

            return solicitud.AgregarRepuesto(repuesto, cantidad);
        }

        public bool CalificarTecnico(Solicitud solicitud, int nota)
        {
            if (solicitud.Estado != EstadoSolicitud.Entregada || solicitud.Tecnico == null)
            {
                Console.WriteLine("Solo se califica al tecnico cuando la solicitud ya fue entregada.");
                return false;
            }

            if (nota < 1 || nota > 5)
            {
                Console.WriteLine("La calificacion debe estar entre 1 y 5.");
                return false;
            }

            solicitud.Tecnico.Calificar(nota);
            return true;
        }

        public void MostrarCargaSemanal()
        {
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay tecnicos registrados.");
                return;
            }

            Console.Write("Tecnico".PadRight(20));
            for (int d = 0; d < 6; d++)
            {
                Console.Write(dias[d].Substring(0, 3).PadLeft(6));
            }
            Console.WriteLine();

            int mayor = -1;
            int filaMayor = 0;
            int diaMayor = 0;

            for (int f = 0; f < tecnicos.Count; f++)
            {
                Console.Write(tecnicos[f].Nombre.PadRight(20));
                for (int d = 0; d < 6; d++)
                {
                    Console.Write(cargaSemanal[f, d].ToString().PadLeft(6));
                    if (cargaSemanal[f, d] > mayor)
                    {
                        mayor = cargaSemanal[f, d];
                        filaMayor = f;
                        diaMayor = d;
                    }
                }
                Console.WriteLine();
            }

            if (mayor <= 0)
            {
                Console.WriteLine("Todavia no hay solicitudes asignadas.");
            }
            else
            {
                Console.WriteLine($"Mayor carga: {tecnicos[filaMayor].Nombre} el {dias[diaMayor]} con {mayor} solicitudes.");
            }
        }

        public void GenerarDatosPrueba()
        {
            Random azar = new Random();

            string[] nombres = { "Ana Lopez", "Carlos Perez", "Luis Chavez", "Maria Gomez", "Jorge Ramirez", "Sofia Morales", "Pedro Castillo", "Lucia Herrera" };
            string[] tiposCliente = { "Estudiante", "Profesional", "Negocio", "Institucion" };
            string[] nombresTecnicos = { "Diego Fuentes", "Andrea Cifuentes", "Mario Barrios", "Karla Monzon" };
            string[] especialidades = { "Portatil", "Escritorio", "Impresora", "Escritorio" };
            string[] marcasPc = { "HP", "Dell", "Lenovo", "Asus", "Acer" };
            string[] marcasImpresora = { "Epson", "Canon", "Brother", "HP" };
            string[] problemas = { "No enciende", "Pantalla azul", "Muy lento", "No imprime", "Hace ruido", "Se apaga solo" };

            int inicioClientes = clientes.Count;
            int inicioEquipos = equipos.Count;
            for (int i = 0; i < 5; i++)
            {
                string codigo = "C" + (clientes.Count + 1).ToString("000");
                while (BuscarCliente(codigo) != null)
                {
                    codigo = codigo + "X";
                }
                string nombre = nombres[clientes.Count % nombres.Length];
                string telefono = azar.Next(30000000, 59999999).ToString();
                string correo = nombre.Split(' ')[0].ToLower() + (clientes.Count + 1) + "@correo.com";
                RegistrarCliente(new Cliente(codigo, nombre, telefono, correo, tiposCliente[azar.Next(tiposCliente.Length)]));
            }

            for (int i = 0; i < nombresTecnicos.Length && tecnicos.Count < 10; i++)
            {
                string codigo = "T" + (tecnicos.Count + 1).ToString("00");
                while (BuscarTecnico(codigo) != null)
                {
                    codigo = codigo + "X";
                }
                string telefono = azar.Next(30000000, 59999999).ToString();
                RegistrarTecnico(new Tecnico(codigo, nombresTecnicos[i], telefono, especialidades[i]));
            }

            string[] nombresRepuestos = { "Disco SSD 480GB", "Memoria RAM 8GB", "Cargador universal", "Fuente de poder", "Cabezal de impresion", "Pasta termica" };
            string[] categorias = { "Almacenamiento", "Memoria", "Energia", "Energia", "Impresion", "Mantenimiento" };
            for (int i = 0; i < nombresRepuestos.Length; i++)
            {
                string codigo = "R" + (repuestos.Count + 1).ToString("000");
                while (BuscarRepuesto(codigo) != null)
                {
                    codigo = codigo + "X";
                }
                double costo = azar.Next(50, 400);
                double precio = costo + azar.Next(20, 150);
                int existencia = azar.Next(3, 15);
                int minimo = azar.Next(1, 4);
                RegistrarRepuesto(new Repuesto(codigo, nombresRepuestos[i], categorias[i], existencia, minimo, costo, precio));
            }

            for (int i = inicioClientes; i < clientes.Count; i++)
            {
                int cantidadEquipos = azar.Next(1, 3);
                for (int k = 0; k < cantidadEquipos; k++)
                {
                    string codigo = "E" + (equipos.Count + 1).ToString("000");
                    while (BuscarEquipo(codigo) != null)
                    {
                        codigo = codigo + "X";
                    }
                    string serie = "SN" + azar.Next(100000, 999999);
                    int tipo = azar.Next(3);
                    Equipo equipo;
                    if (tipo == 0)
                    {
                        equipo = new Portatil(codigo, serie, marcasPc[azar.Next(marcasPc.Length)], "Modelo " + azar.Next(100, 999), clientes[i], "Bueno", "Mochila", azar.Next(2) == 1);
                    }
                    else if (tipo == 1)
                    {
                        equipo = new Escritorio(codigo, serie, marcasPc[azar.Next(marcasPc.Length)], "Modelo " + azar.Next(100, 999), clientes[i], "Con rayones", "Teclado y mouse", azar.Next(2) == 1);
                    }
                    else
                    {
                        equipo = new Impresora(codigo, serie, marcasImpresora[azar.Next(marcasImpresora.Length)], "Modelo " + azar.Next(100, 999), clientes[i], "Bueno", "Cable USB", "Tinta");
                    }
                    RegistrarEquipo(equipo);
                }
            }

            int creadas = 0;
            for (int i = inicioEquipos; i < equipos.Count; i++)
            {
                string prioridad = "Normal";
                if (azar.Next(3) == 0)
                {
                    prioridad = "Urgente";
                }

                Solicitud solicitud = CrearSolicitud(equipos[i].Propietario.Codigo, equipos[i].Codigo, problemas[azar.Next(problemas.Length)], prioridad);
                if (solicitud != null)
                {
                    solicitud.FechaRecepcion = DateTime.Now.AddDays(-azar.Next(0, 20)).AddHours(-azar.Next(0, 10));
                    creadas = creadas + 1;
                }
            }

            int atender = creadas - 2;
            for (int i = 0; i < atender; i++)
            {
                Solicitud solicitud = AtenderSiguiente(azar.Next(6));
                if (solicitud == null)
                {
                    continue;
                }

                int avance = azar.Next(4);
                if (avance >= 1)
                {
                    CambiarEstado(solicitud, EstadoSolicitud.EnDiagnostico);
                    RegistrarDiagnostico(solicitud, "Revision completa del equipo", azar.Next(50, 300));
                    Repuesto repuesto = repuestos[azar.Next(repuestos.Count)];
                    UsarRepuesto(solicitud, repuesto.Codigo, azar.Next(1, 3));
                }
                if (avance >= 2)
                {
                    CambiarEstado(solicitud, EstadoSolicitud.EnReparacion);
                    CambiarEstado(solicitud, EstadoSolicitud.ListaParaEntregar);
                }
                if (avance >= 3)
                {
                    CambiarEstado(solicitud, EstadoSolicitud.Entregada);
                    CalificarTecnico(solicitud, azar.Next(3, 6));
                }
            }

            Console.WriteLine($"Datos generados: {clientes.Count} clientes, {tecnicos.Count} tecnicos, {equipos.Count} equipos, {repuestos.Count} repuestos y {solicitudes.Count} solicitudes.");
        }
    }
}
