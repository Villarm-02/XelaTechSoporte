using System;

namespace XelaTechSoporte
{
    public class Escritorio : Equipo
    {
        private bool incluyeMonitor;

        public bool IncluyeMonitor
        {
            get { return incluyeMonitor; }
            set { incluyeMonitor = value; }
        }

        public Escritorio(string codigo, string serie, string marca, string modelo, Cliente propietario, string estadoFisico, string accesorios, bool incluyeMonitor)
            : base(codigo, serie, marca, modelo, propietario, estadoFisico, accesorios)
        {
            IncluyeMonitor = incluyeMonitor;
            tipo = "Escritorio";
        }

        // Costo de revision de una computadora de escritorio
        public override double CostoRevision()
        {
            return 125;
        }

        public override void MostrarDatos()
        {
            base.MostrarDatos();
            if (IncluyeMonitor == true)
            {
                Console.WriteLine("   Incluye monitor: Si");
            }
            else
            {
                Console.WriteLine("   Incluye monitor: No");
            }
        }
    }
}
