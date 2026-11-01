using System;

namespace XelaTechSoporte
{
    public class Cliente : Persona
    {
        private string correo;
        private string tipo;
        private int cantidadSolicitudes;

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        // Estudiante, Profesional, Negocio o Institucion
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public int CantidadSolicitudes
        {
            get { return cantidadSolicitudes; }
        }

        public Cliente(string codigo, string nombre, string telefono, string correo, string tipo)
            : base(codigo, nombre, telefono)
        {
            Correo = correo;
            Tipo = tipo;
            cantidadSolicitudes = 0;
        }

        public void SumarSolicitud()
        {
            cantidadSolicitudes = cantidadSolicitudes + 1;
        }

        public override void MostrarDatos()
        {
            Console.WriteLine($"[CLIENTE] Codigo: {Codigo} | Nombre: {Nombre} | Telefono: {Telefono} | Correo: {Correo} | Tipo: {Tipo} | Solicitudes: {CantidadSolicitudes}");
        }
    }
}
