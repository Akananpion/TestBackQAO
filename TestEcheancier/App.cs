using System;
using System.Threading.Tasks;
using TestEcheancier.Models;

namespace TestEcheancier
{
    public class App
    {

        public void Run(Arguments arguments)
        {
            Console.Write("Date d�but : \n");
            Console.Write(string.Concat(arguments.DateDebut.ToShortDateString(),"\n"));
		}
    }
}
