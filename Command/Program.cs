using System;
using System.Collections.Generic;
using System.Threading;

namespace Command
{
    enum StatusPedido
    {
        Aberto,
        Pago,
        Finalizado
    }
    
    class Pedido
    {
        public string Cliente { get; private set; }
        public decimal Valor { get; private set; }
        public StatusPedido Status { get; private set; }
        public DateTime DataFinalizacao { get; private set; }

        public Pedido(string cliente, decimal valor)
        {
            Cliente = cliente;
            Valor = valor;
            Status = StatusPedido.Aberto;
        }

        public void Pago()
        {
            Status = StatusPedido.Pago;
            Console.WriteLine("Pedido cliente {0} pago", Cliente);
        }

        public void Finaliza()
        {
            DataFinalizacao = DateTime.Now;
            Status = StatusPedido.Finalizado;
            Console.WriteLine("Pedido cliente {0} finalizado", Cliente);
        }
    }

    interface IComando
    {
        void Executa();
    }

    class FinalizaPedido : IComando
    {
        public Pedido Pedido { get; private set; }

        public FinalizaPedido(Pedido pedido)
        {
            Pedido = pedido;
        }
        public void Executa()
        {
            Pedido.Finaliza();
        }
    }

    class PagaPedido : IComando
    {
        public Pedido Pedido { get; private set; }

        public PagaPedido(Pedido pedido)
        {
            Pedido = pedido;
        }
        public void Executa()
        {
            Pedido.Pago();
        }
    }

    class ProcessaPedidos
    {
        private List<IComando> comandos = new List<IComando>();

        public void Adiciona(IComando comando)
        {
            comandos.Add(comando);
        }

        public void Processa()
        {
            comandos.ForEach(comando =>
            {
                comando.Executa();
            });
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var pedido1 = new Pedido("Pedido 1", 100);
            var pedido2 = new Pedido("Pedido 2", 200);

            var processaPedidos = new ProcessaPedidos();

            processaPedidos.Adiciona(new PagaPedido(pedido1));
            processaPedidos.Adiciona(new PagaPedido(pedido2));
            processaPedidos.Adiciona(new FinalizaPedido(pedido2));
            processaPedidos.Adiciona(new FinalizaPedido(pedido1));

            processaPedidos.Processa();

        }
    }
}
