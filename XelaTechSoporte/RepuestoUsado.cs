using System;

namespace XelaTechSoporte
{
    public class RepuestoUsado
    {
        private Repuesto repuesto;
        private int cantidad;

        public Repuesto Repuesto
        {
            get { return repuesto; }
            set { repuesto = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public RepuestoUsado(Repuesto repuesto, int cantidad)
        {
            Repuesto = repuesto;
            Cantidad = cantidad;
        }

        // Se calcula el subtotal del repuesto usado
        public double Subtotal()
        {
            return repuesto.PrecioVenta * cantidad;
        }

        public void MostrarDatos()
        {
            Console.WriteLine($"   {Repuesto.Nombre} x {Cantidad} = Q{Subtotal():0.00}");
        }
    }
}
