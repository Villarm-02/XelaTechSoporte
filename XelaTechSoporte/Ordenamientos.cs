using System;
using System.Collections.Generic;

namespace XelaTechSoporte
{
    public static class Ordenamientos
    {
        private static int intercambios;

        public static int Intercambios
        {
            get { return intercambios; }
        }

        public static List<Solicitud> BubblePorTotal(List<Solicitud> lista, bool ascendente)
        {
            List<Solicitud> datos = new List<Solicitud>(lista);
            intercambios = 0;
            int n = datos.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    bool cambiar;
                    if (ascendente == true)
                    {
                        cambiar = datos[j].CalcularTotal() > datos[j + 1].CalcularTotal();
                    }
                    else
                    {
                        cambiar = datos[j].CalcularTotal() < datos[j + 1].CalcularTotal();
                    }

                    if (cambiar == true)
                    {
                        Solicitud temporal = datos[j];
                        datos[j] = datos[j + 1];
                        datos[j + 1] = temporal;
                        intercambios = intercambios + 1;
                    }
                }
            }
            return datos;
        }

        public static List<Tecnico> SelectionPorServicios(List<Tecnico> lista, bool ascendente)
        {
            List<Tecnico> datos = new List<Tecnico>(lista);
            intercambios = 0;
            int n = datos.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int elegido = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (ascendente == true)
                    {
                        if (datos[j].ServiciosFinalizados < datos[elegido].ServiciosFinalizados)
                        {
                            elegido = j;
                        }
                    }
                    else
                    {
                        if (datos[j].ServiciosFinalizados > datos[elegido].ServiciosFinalizados)
                        {
                            elegido = j;
                        }
                    }
                }

                if (elegido != i)
                {
                    Tecnico temporal = datos[i];
                    datos[i] = datos[elegido];
                    datos[elegido] = temporal;
                    intercambios = intercambios + 1;
                }
            }
            return datos;
        }

        public static List<Repuesto> ShellPorExistencia(List<Repuesto> lista, bool ascendente)
        {
            List<Repuesto> datos = new List<Repuesto>(lista);
            intercambios = 0;
            int n = datos.Count;
            int salto = n / 2;

            while (salto > 0)
            {
                for (int i = salto; i < n; i++)
                {
                    Repuesto actual = datos[i];
                    int j = i;

                    while (j >= salto && DebeIrDespues(datos[j - salto], actual, ascendente))
                    {
                        datos[j] = datos[j - salto];
                        j = j - salto;
                        intercambios = intercambios + 1;
                    }
                    datos[j] = actual;
                }
                salto = salto / 2;
            }
            return datos;
        }

        private static bool DebeIrDespues(Repuesto a, Repuesto b, bool ascendente)
        {
            if (ascendente == true)
            {
                return a.Existencia > b.Existencia;
            }
            return a.Existencia < b.Existencia;
        }

        public static List<Solicitud> QuickPorFecha(List<Solicitud> lista, bool ascendente)
        {
            List<Solicitud> datos = new List<Solicitud>(lista);
            intercambios = 0;
            Quick(datos, 0, datos.Count - 1, ascendente);
            return datos;
        }

        private static void Quick(List<Solicitud> datos, int inicio, int fin, bool ascendente)
        {
            if (inicio >= fin)
            {
                return;
            }

            DateTime pivote = datos[fin].FechaRecepcion;
            int i = inicio - 1;

            for (int j = inicio; j < fin; j++)
            {
                bool vaAntes;
                if (ascendente == true)
                {
                    vaAntes = datos[j].FechaRecepcion <= pivote;
                }
                else
                {
                    vaAntes = datos[j].FechaRecepcion >= pivote;
                }

                if (vaAntes == true)
                {
                    i = i + 1;
                    Intercambiar(datos, i, j);
                }
            }
            Intercambiar(datos, i + 1, fin);

            Quick(datos, inicio, i, ascendente);
            Quick(datos, i + 2, fin, ascendente);
        }

        private static void Intercambiar(List<Solicitud> datos, int a, int b)
        {
            if (a == b)
            {
                return;
            }
            Solicitud temporal = datos[a];
            datos[a] = datos[b];
            datos[b] = temporal;
            intercambios = intercambios + 1;
        }
    }
}
