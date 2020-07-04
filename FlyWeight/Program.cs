using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;

namespace FlyWeight
{
    interface INota
    {
        ushort Frequencia { get; }
        string Nome { get;  }
    }

    class Do : INota
    {
        public ushort Frequencia => 262;
        public string Nome => "Dó";
    }
    class Re : INota
    {
        public ushort Frequencia => 294;
        public string Nome => "Ré";
    }
    class Mi : INota
    {
        public ushort Frequencia => 330;
        public string Nome => "Mi";
    }
    class Fa : INota
    {
        public ushort Frequencia => 349;
        public string Nome => "Fá";
    }
    class Sol : INota
    {
        public ushort Frequencia => 392;
        public string Nome => "Sol";
    }
    class La : INota
    {
        public ushort Frequencia => 440;
        public string Nome => "Lá";
    }
    class Si : INota
    {
        public ushort Frequencia => 490;
        public string Nome => "Si";
    }

    class NotasMusicais
    {
        private Dictionary<string, INota> _Notas = new Dictionary<string, INota>()
        {
            { "do", new Do() },
            { "re", new Re() },
            { "mi", new Mi() },
            { "fa", new Fa() },
            { "sol", new Sol() },
            { "la", new La() },
            { "si", new Si() },
        };

        public INota Pega(string nota)
        {
            nota = nota.ToLower();
            if (_Notas.ContainsKey(nota))
            {
                return _Notas[nota];
            }
            else
            {
                throw new Exception("Nota não existente");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Flyweight");
            Console.WriteLine("Playsound ");
            var notas = new NotasMusicais();
            var doReMiFaFa = new List<INota>()
            {
                notas.Pega("do"),
                notas.Pega("re"),
                notas.Pega("mi"),
                notas.Pega("fa"),
                notas.Pega("fa"),
                notas.Pega("fa"),

                notas.Pega("do"),
                notas.Pega("re"),
                notas.Pega("do"),
                notas.Pega("re"),
                notas.Pega("re"),
                notas.Pega("re"),

                notas.Pega("do"),
                notas.Pega("sol"),
                notas.Pega("fa"),
                notas.Pega("mi"),
                notas.Pega("mi"),
                notas.Pega("mi"),

                notas.Pega("do"),
                notas.Pega("re"),
                notas.Pega("mi"),
                notas.Pega("fa"),
                notas.Pega("fa"),
                notas.Pega("fa"),
            };

            doReMiFaFa.ForEach(nota =>
            {
                Console.WriteLine(nota.Nome);
                Console.Beep(nota.Frequencia, 500);
            });
        }
    }
}
