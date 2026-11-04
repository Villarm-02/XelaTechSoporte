using System;

namespace XelaTechSoporte
{
    public class Portatil : Equipo
    {
        private bool incluyeCargador;

        public bool IncluyeCargador
        {
            get { return incluyeCargador; }
            set { incluyeCargador = value; }
        }

        public Portatil(string codigo, string serie, string marca, string modelo, Cliente propietario, string estadoFisico, string accesorios, bool incluyeCargador)
            : base(codigo, serie, marca, modelo, propietario, estadoFisico, accesorios)
        {
            IncluyeCargador = incluyeCargador;
            tipo = "Portatil";
        }

        // Costo de revision de un portatil
        public override double CostoRevision()
        {
            return 150;
        }

        public override void MostrarDatos()
        {
            base.MostrarDatos();
            if (IncluyeCargador == true)
            {
                Console.WriteLine("   Incluye cargador: Si");
            }
            else
            {
                Console.WriteLine("   Incluye cargador: No");
            }
        }
    }
}
