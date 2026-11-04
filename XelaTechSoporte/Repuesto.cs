using System;

namespace XelaTechSoporte
{
    public class Repuesto
    {
        private string codigo;
        private string nombre;
        private string categoria;
        private int existencia;
        private int existenciaMinima;
        private double costo;
        private double precioVenta;
        private int cantidadUsada;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public int Existencia
        {
            get { return existencia; }
            set { existencia = value; }
        }

        public int ExistenciaMinima
        {
            get { return existenciaMinima; }
            set { existenciaMinima = value; }
        }

        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        public double PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }

        public int CantidadUsada
        {
            get { return cantidadUsada; }
        }

        public Repuesto(string codigo, string nombre, string categoria, int existencia, int existenciaMinima, double costo, double precioVenta)
        {
            Codigo = codigo;
            Nombre = nombre;
            Categoria = categoria;
            Existencia = existencia;
            ExistenciaMinima = existenciaMinima;
            Costo = costo;
            PrecioVenta = precioVenta;
            cantidadUsada = 0;
        }

        // Se agrego la funcion para descontar repuestos de la existencia
        public bool Descontar(int cantidad)
        {
            if (cantidad <= 0)
            {
                Console.WriteLine("La cantidad debe ser mayor que cero.");
                return false;
            }

            if (cantidad > existencia)
            {
                Console.WriteLine($"No hay suficiente existencia de {Nombre}. Solo quedan {Existencia}.");
                return false;
            }

            existencia = existencia - cantidad;
            cantidadUsada = cantidadUsada + cantidad;

            if (EstaEnMinimo() == true)
            {
                Console.WriteLine($"Aviso: el repuesto {Nombre} llego a su existencia minima ({Existencia} disponibles).");
            }
            return true;
        }

        public bool EstaEnMinimo()
        {
            if (existencia <= existenciaMinima)
            {
                return true;
            }
            return false;
        }

        public void MostrarDatos()
        {
            Console.WriteLine($"[REPUESTO] Codigo: {Codigo} | Nombre: {Nombre} | Categoria: {Categoria} | Existencia: {Existencia} | Minima: {ExistenciaMinima} | Costo: Q{Costo:0.00} | Venta: Q{PrecioVenta:0.00}");
        }
    }
}
