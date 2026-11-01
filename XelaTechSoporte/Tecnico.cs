namespace XelaTechSoporte
{
    public class Tecnico : Persona
    {
        // Un técnico no puede tener más de 3 trabajos activos al mismo tiempo
        public const int MaxTrabajos = 3;

        private string especialidad;
        private bool disponible;
        private int trabajosActivos;
        private int serviciosFinalizados;

        // Arreglo de 5 posiciones: cuenta cuántas calificaciones de 1, 2, 3, 4 y 5 recibió.
        // La posición 0 guarda las de 1 estrella y la posición 4 las de 5 estrellas.
        private int[] calificaciones;

        // Especialidad: Portátil, Escritorio o Impresora
        public string Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
        }

        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }

        public int TrabajosActivos
        {
            get { return trabajosActivos; }
            set { trabajosActivos = value; }
        }

        public int ServiciosFinalizados
        {
            get { return serviciosFinalizados; }
            set { serviciosFinalizados = value; }
        }

        public int[] Calificaciones
        {
            get { return calificaciones; }
        }

        public Tecnico(string codigo, string nombre, string telefono, string especialidad)
            : base(codigo, nombre, telefono)
        {
            this.especialidad = especialidad;
            disponible = true;
            trabajosActivos = 0;
            serviciosFinalizados = 0;
            calificaciones = new int[5];
        }

        // Revisa si el técnico puede recibir un equipo de cierto tipo
        public bool PuedeAtender(string tipoEquipo)
        {
            return disponible && trabajosActivos < MaxTrabajos && especialidad == tipoEquipo;
        }

        // Guarda una calificación de 1 a 5. Devuelve false si el número no es válido.
        public bool Calificar(int nota)
        {
            if (nota < 1 || nota > 5)
            {
                return false;
            }
            calificaciones[nota - 1]++;
            return true;
        }

        // Promedio de las calificaciones recibidas. Si no tiene ninguna, devuelve 0.
        public double Promedio()
        {
            int total = 0;
            int suma = 0;
            for (int i = 0; i < calificaciones.Length; i++)
            {
                total = total + calificaciones[i];
                suma = suma + calificaciones[i] * (i + 1);
            }
            if (total == 0)
            {
                return 0;
            }
            return (double)suma / total;
        }

        public override void MostrarDatos()
        {
            string estado = disponible ? "Disponible" : "No disponible";
            Console.WriteLine("Código: " + codigo + " | Nombre: " + nombre + " | Teléfono: " + telefono
                + " | Especialidad: " + especialidad + " | " + estado
                + " | Activos: " + trabajosActivos + "/" + MaxTrabajos
                + " | Finalizados: " + serviciosFinalizados
                + " | Promedio: " + Promedio().ToString("0.0"));
        }
    }
}
