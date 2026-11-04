using System;

namespace XelaTechSoporte
{
    public class Tecnico : Persona
    {
        private string especialidad;
        private bool disponible;
        private int trabajosActivos;
        private int serviciosFinalizados;

        private int[] calificaciones = new int[5];

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
            Especialidad = especialidad;
            disponible = true;
            trabajosActivos = 0;
            serviciosFinalizados = 0;
        }

        // Se revisa si el tecnico puede atender ese tipo de equipo
        public bool PuedeAtender(string tipoEquipo)
        {
            if (disponible == true && trabajosActivos < 3 && especialidad == tipoEquipo)
            {
                return true;
            }
            return false;
        }

        public void Calificar(int nota)
        {
            if (nota < 1 || nota > 5)
            {
                Console.WriteLine("La calificacion debe estar entre 1 y 5.");
            }
            else
            {
                calificaciones[nota - 1] = calificaciones[nota - 1] + 1;
            }
        }

        // Se calcula el promedio de las calificaciones
        public double Promedio()
        {
            double suma = 0;
            int total = 0;

            for (int i = 0; i < calificaciones.Length; i++)
            {
                suma = suma + calificaciones[i] * (i + 1);
                total = total + calificaciones[i];
            }

            if (total == 0)
            {
                return 0;
            }
            return suma / total;
        }

        public override void MostrarDatos()
        {
            string estado = "No disponible";
            if (disponible == true)
            {
                estado = "Disponible";
            }

            Console.WriteLine($"[TECNICO] Codigo: {Codigo} | Nombre: {Nombre} | Especialidad: {Especialidad} | Estado: {estado} | Activos: {TrabajosActivos} | Finalizados: {ServiciosFinalizados} | Promedio: {Promedio():0.0}");
        }
    }
}
