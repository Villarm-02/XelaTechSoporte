using System;
using System.Collections.Generic;

namespace XelaTechSoporte
{
    public static class Busquedas
    {
        private static int comparaciones;

        public static int Comparaciones
        {
            get { return comparaciones; }
        }

        // Se agrego la busqueda secuencial de clientes por nombre
        public static List<Cliente> ClientesPorNombre(List<Cliente> clientes, string texto)
        {
            List<Cliente> encontrados = new List<Cliente>();
            comparaciones = 0;

            for (int i = 0; i < clientes.Count; i++)
            {
                comparaciones = comparaciones + 1;
                if (clientes[i].Nombre.ToLower().Contains(texto.ToLower()))
                {
                    encontrados.Add(clientes[i]);
                }
            }
            return encontrados;
        }

        // Se agrego la busqueda secuencial de solicitudes por estado
        public static List<Solicitud> SolicitudesPorEstado(List<Solicitud> solicitudes, EstadoSolicitud estado)
        {
            List<Solicitud> encontradas = new List<Solicitud>();
            comparaciones = 0;

            for (int i = 0; i < solicitudes.Count; i++)
            {
                comparaciones = comparaciones + 1;
                if (solicitudes[i].Estado == estado)
                {
                    encontradas.Add(solicitudes[i]);
                }
            }
            return encontradas;
        }

        // Se agrego la busqueda secuencial de equipos por marca
        public static List<Equipo> EquiposPorMarca(List<Equipo> equipos, string marca)
        {
            List<Equipo> encontrados = new List<Equipo>();
            comparaciones = 0;

            for (int i = 0; i < equipos.Count; i++)
            {
                comparaciones = comparaciones + 1;
                if (equipos[i].Marca.ToLower() == marca.ToLower())
                {
                    encontrados.Add(equipos[i]);
                }
            }
            return encontrados;
        }

        // Se ordenan las solicitudes por codigo para poder usar la busqueda binaria
        public static Solicitud[] OrdenarPorCodigo(List<Solicitud> solicitudes)
        {
            Solicitud[] arreglo = solicitudes.ToArray();

            for (int i = 1; i < arreglo.Length; i++)
            {
                Solicitud actual = arreglo[i];
                int j = i - 1;
                while (j >= 0 && string.Compare(arreglo[j].Codigo, actual.Codigo) > 0)
                {
                    arreglo[j + 1] = arreglo[j];
                    j = j - 1;
                }
                arreglo[j + 1] = actual;
            }
            return arreglo;
        }

        public static bool EstaOrdenadoPorCodigo(Solicitud[] arreglo)
        {
            for (int i = 0; i < arreglo.Length - 1; i++)
            {
                if (string.Compare(arreglo[i].Codigo, arreglo[i + 1].Codigo) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Se agrego la busqueda binaria de una solicitud por codigo
        public static Solicitud BinariaPorCodigo(Solicitud[] arreglo, string codigo)
        {
            comparaciones = 0;

            if (EstaOrdenadoPorCodigo(arreglo) == false)
            {
                Console.WriteLine("Los datos no estan ordenados por codigo, no se puede usar busqueda binaria.");
                return null;
            }

            int izquierda = 0;
            int derecha = arreglo.Length - 1;

            while (izquierda <= derecha)
            {
                int medio = (izquierda + derecha) / 2;
                comparaciones = comparaciones + 1;
                int resultado = string.Compare(arreglo[medio].Codigo, codigo);

                if (resultado == 0)
                {
                    return arreglo[medio];
                }
                else if (resultado < 0)
                {
                    izquierda = medio + 1;
                }
                else
                {
                    derecha = medio - 1;
                }
            }
            return null;
        }
    }
}
