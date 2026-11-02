using System;

namespace XelaTechSoporte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "XelaTech Soporte";
            Sistema sistema = new Sistema();
            Menu menu = new Menu(sistema);
            menu.MostrarMenu();
        }
    }
}
