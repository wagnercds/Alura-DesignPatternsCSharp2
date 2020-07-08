using System;
using System.Collections.Generic;

namespace Memento
{
    enum TipoContrato
    {
        Novo,
        EmAndamento,
        Acertado,
        Concluido
    }
    class Contrato
    {
        public string Nome { get; private set; }
        public DateTime Data { get; private set; }
        public TipoContrato Tipo { get; private set; }

        public Contrato(string nome, DateTime data, TipoContrato tipo)
        {
            this.Nome = nome;
            this.Data = data;
            this.Tipo = tipo;
        }

        public void Avanca()
        {
            if (this.Tipo == TipoContrato.Concluido)
                throw new Exception("Contrato já foi concluido");

            this.Tipo++;
        }

        public Estado SalvaEstado()
        {
            return new Estado(new Contrato(this.Nome, this.Data, this.Tipo));
        }
    }

    class Estado
    {
        public Contrato Contrato { get; private set; }
        public Estado(Contrato contrato)
        {
            this.Contrato = contrato;
        }
    }

    class Historico
    {
        private List<Estado> _Estados = new List<Estado>();

        public Estado Pega(ushort indice)
        {
            return _Estados[indice];
        }

        public void Adiciona(Estado estado)
        {
            _Estados.Add(estado);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var contrato = new Contrato("teste", DateTime.Now, TipoContrato.Novo);
            var historico = new Historico();

            for (byte i = 0; i < 4; i++)
            {
                Console.WriteLine("Tipo contrato {0}", contrato.Tipo);
                try
                {
                    contrato.Avanca();
                    historico.Adiciona(contrato.SalvaEstado());
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Gerou uma exceção: {0}", ex.Message);
                }
            }

            Estado estadoTeste = historico.Pega(1);
            Console.WriteLine("Histórico: {0}", estadoTeste.Contrato.Tipo);
        }
    }
}
