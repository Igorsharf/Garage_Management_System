using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class Program
    {
        private static readonly GarageUI r_GarageUI = new GarageUI();

        public static void Main()
        {
            while (!r_GarageUI.QuitProgram)
            {
                Console.Clear();
                r_GarageUI.StartMenu();
            }
        }
    }
}
