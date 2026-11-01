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

        // Tipo de cliente: Estudiante, Profesional, Negocio o Institución
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
            this.correo = correo;
            this.tipo = tipo;
            cantidadSolicitudes = 0;
        }

        // Se llama cada vez que el cliente deja una nueva solicitud
        public void SumarSolicitud()
        {
            cantidadSolicitudes++;
        }

        public override void MostrarDatos()
        {
            Console.WriteLine("Código: " + codigo + " | Nombre: " + nombre + " | Teléfono: " + telefono
                + " | Correo: " + correo + " | Tipo: " + tipo + " | Solicitudes: " + cantidadSolicitudes);
        }
    }
}
