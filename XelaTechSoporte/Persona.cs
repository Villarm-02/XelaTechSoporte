using System;

namespace XelaTechSoporte
{
    public abstract class Persona
    {
        protected string codigo;
        protected string nombre;
        protected string telefono;

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

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public Persona(string codigo, string nombre, string telefono)
        {
            Codigo = codigo;
            Nombre = nombre;
            Telefono = telefono;
        }

        public abstract void MostrarDatos();
    }
}
