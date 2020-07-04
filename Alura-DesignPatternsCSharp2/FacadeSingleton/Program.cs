using System;
using System.Linq.Expressions;

namespace FacadeSingleton
{
    class Servico
    {

    }

    class FactoryServico
    {
        private Servico servico = new Servico();

        public Servico PegaInstancia => servico;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
