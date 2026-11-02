using System;

namespace XelaTechSoporte
{
    public class Impresora : Equipo
    {
        private string tipoImpresion;

        public string TipoImpresion
        {
            get { return tipoImpresion; }
            set { tipoImpresion = value; }
        }

        public Impresora(string codigo, string serie, string marca, string modelo, Cliente propietario, string estadoFisico, string accesorios, string tipoImpresion)
            : base(codigo, serie, marca, modelo, propietario, estadoFisico, accesorios)
        {
            TipoImpresion = tipoImpresion;
            tipo = "Impresora";
        }

        public override double CostoRevision()
        {
            return 100;
        }

        public override void MostrarDatos()
        {
            base.MostrarDatos();
            Console.WriteLine($"   Tipo de impresion: {TipoImpresion}");
        }
    }
}
