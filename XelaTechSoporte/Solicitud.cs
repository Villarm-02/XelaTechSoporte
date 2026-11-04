using System;
using System.Collections.Generic;

namespace XelaTechSoporte
{
    public class Solicitud
    {
        private string codigo;
        private Cliente cliente;
        private Equipo equipo;
        private string problema;
        private DateTime fechaRecepcion;
        private string prioridad;
        private EstadoSolicitud estado;
        private Tecnico tecnico;
        private string diagnostico;
        private List<RepuestoUsado> repuestos = new List<RepuestoUsado>();
        private double manoObra;
        private Stack<EstadoSolicitud> historial = new Stack<EstadoSolicitud>();

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public Equipo Equipo
        {
            get { return equipo; }
            set { equipo = value; }
        }

        public string Problema
        {
            get { return problema; }
            set { problema = value; }
        }

        public DateTime FechaRecepcion
        {
            get { return fechaRecepcion; }
            set { fechaRecepcion = value; }
        }

        public string Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }

        public EstadoSolicitud Estado
        {
            get { return estado; }
        }

        public Tecnico Tecnico
        {
            get { return tecnico; }
            set { tecnico = value; }
        }

        public string Diagnostico
        {
            get { return diagnostico; }
            set { diagnostico = value; }
        }

        public List<RepuestoUsado> Repuestos
        {
            get { return repuestos; }
        }

        public double ManoObra
        {
            get { return manoObra; }
            set { manoObra = value; }
        }

        public Stack<EstadoSolicitud> Historial
        {
            get { return historial; }
        }

        public Solicitud(string codigo, Cliente cliente, Equipo equipo, string problema)
        {
            Codigo = codigo;
            Cliente = cliente;
            Equipo = equipo;
            Problema = problema;
            FechaRecepcion = DateTime.Now;
            Prioridad = "Normal";
            estado = EstadoSolicitud.Recibida;
            tecnico = null;
            diagnostico = "";
            manoObra = 0;
        }

        public Solicitud(string codigo, Cliente cliente, Equipo equipo, string problema, string prioridad)
            : this(codigo, cliente, equipo, problema)
        {
            Prioridad = prioridad;
        }

        public bool EstaActiva()
        {
            if (estado == EstadoSolicitud.Entregada || estado == EstadoSolicitud.Cancelada)
            {
                return false;
            }
            return true;
        }

        // Solo deja pasar al siguiente estado o cancelar
        public bool CambiarEstado(EstadoSolicitud nuevoEstado)
        {
            if (EstaActiva() == false)
            {
                Console.WriteLine("La solicitud ya fue entregada o cancelada, no se puede cambiar.");
                return false;
            }

            bool esSiguiente = (int)nuevoEstado == (int)estado + 1;
            bool esCancelar = nuevoEstado == EstadoSolicitud.Cancelada;

            if (esSiguiente == false && esCancelar == false)
            {
                Console.WriteLine($"No se puede pasar de {estado} a {nuevoEstado}. Los estados deben seguir el orden.");
                return false;
            }

            historial.Push(estado);
            estado = nuevoEstado;
            return true;
        }

        // Se agrego la funcion para deshacer el ultimo cambio con la pila
        public bool DeshacerCambio()
        {
            if (EstaActiva() == false)
            {
                Console.WriteLine("No se puede deshacer porque la solicitud ya fue entregada o cancelada.");
                return false;
            }

            if (historial.Count == 0)
            {
                Console.WriteLine("No hay cambios para deshacer.");
                return false;
            }

            estado = historial.Pop();
            return true;
        }

        public bool AgregarRepuesto(Repuesto repuesto, int cantidad)
        {
            if (repuesto.Descontar(cantidad) == false)
            {
                return false;
            }

            repuestos.Add(new RepuestoUsado(repuesto, cantidad));
            return true;
        }

        // Se calcula el total: revision + mano de obra + repuestos
        public double CalcularTotal()
        {
            double total = equipo.CostoRevision() + manoObra;

            for (int i = 0; i < repuestos.Count; i++)
            {
                total = total + repuestos[i].Subtotal();
            }
            return total;
        }

        public void MostrarHistorial()
        {
            if (historial.Count == 0)
            {
                Console.WriteLine("La solicitud no tiene cambios de estado.");
                return;
            }

            Console.WriteLine($"Historial de {Codigo} (del mas reciente al mas antiguo):");
            foreach (EstadoSolicitud anterior in historial)
            {
                Console.WriteLine($"   {anterior}");
            }
        }

        public void MostrarDatos()
        {
            string nombreTecnico = "Sin asignar";
            if (tecnico != null)
            {
                nombreTecnico = tecnico.Nombre;
            }

            Console.WriteLine($"[SOLICITUD] Codigo: {Codigo} | Cliente: {Cliente.Nombre} | Equipo: {Equipo.Tipo} {Equipo.Marca} | Fecha: {FechaRecepcion:dd/MM/yyyy} | Prioridad: {Prioridad} | Estado: {Estado} | Tecnico: {nombreTecnico} | Total: Q{CalcularTotal():0.00}");
        }
    }
}
