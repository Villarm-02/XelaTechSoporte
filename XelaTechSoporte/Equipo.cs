using System;

namespace XelaTechSoporte
{
    public abstract class Equipo
    {
        protected string codigo;
        protected string serie;
        protected string marca;
        protected string modelo;
        protected Cliente propietario;
        protected string estadoFisico;
        protected string accesorios;
        protected string tipo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Serie
        {
            get { return serie; }
            set { serie = value; }
        }

        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        public Cliente Propietario
        {
            get { return propietario; }
            set { propietario = value; }
        }

        public string EstadoFisico
        {
            get { return estadoFisico; }
            set { estadoFisico = value; }
        }

        public string Accesorios
        {
            get { return accesorios; }
            set { accesorios = value; }
        }

        public string Tipo
        {
            get { return tipo; }
        }

        public Equipo(string codigo, string serie, string marca, string modelo, Cliente propietario, string estadoFisico, string accesorios)
        {
            Codigo = codigo;
            Serie = serie;
            Marca = marca;
            Modelo = modelo;
            Propietario = propietario;
            EstadoFisico = estadoFisico;
            Accesorios = accesorios;
        }

        public abstract double CostoRevision();

        public virtual void MostrarDatos()
        {
            Console.WriteLine($"[{Tipo.ToUpper()}] Codigo: {Codigo} | Serie: {Serie} | Marca: {Marca} | Modelo: {Modelo} | Propietario: {Propietario.Nombre} | Estado: {EstadoFisico} | Accesorios: {Accesorios} | Revision: Q{CostoRevision():0.00}");
        }
    }
}
