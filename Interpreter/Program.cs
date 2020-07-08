using System;
using System.ComponentModel;
using System.Threading;

namespace Interpreter
{
    interface IExpressao
    {
        double Avalia();
    }

    class Numero : IExpressao
    {
        public double Valor { get; private set; }

        public Numero(double valor)
        {
            this.Valor = valor;
        }

        public double Avalia()
        {
            return this.Valor;
        }
    }

    abstract class BaseExpressao: IExpressao
    {
        public IExpressao Esquerda { get; private set; }
        public IExpressao Direita { get; private set; }

        public BaseExpressao(IExpressao esquerda, IExpressao direita)
        {
            this.Esquerda = esquerda;
            this.Direita = direita;
        }

        public abstract double Avalia();
    }

    

    class Soma: BaseExpressao
    {
        public Soma(IExpressao esquerda, IExpressao direita): base(esquerda, direita)
        {

        } 

        public override double Avalia()
        {
            return this.Esquerda.Avalia() + this.Direita.Avalia();
        }

    }

    class Subtracao : BaseExpressao
    {
        public Subtracao(IExpressao esquerda, IExpressao direita): base(esquerda, direita)
        {
            
        }

        public override double Avalia()
        {
            return this.Esquerda.Avalia() - this.Direita.Avalia();
        }
    }

    class Multiplicacao : BaseExpressao
    {
        public Multiplicacao(IExpressao esquerda, IExpressao direita): base(esquerda, direita)
        {
           
        }

        public override double Avalia()
        {
            return this.Esquerda.Avalia() * this.Direita.Avalia();
        }
    }

    class Divisao : BaseExpressao
    {
        public Divisao(IExpressao esquerda, IExpressao direita) : base(esquerda, direita)
        {

        }

        public override double Avalia()
        {
            return this.Esquerda.Avalia() / this.Direita.Avalia();
        }
    }

    class RaizQuadrada: IExpressao
    {
        public IExpressao Valor { get; private set; }
        public RaizQuadrada(IExpressao valor)
        {
            this.Valor = valor;
        }

        public double Avalia()
        {
            return Math.Sqrt(this.Valor.Avalia());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Resolvendo a expressão (2 + 3) - 2  = 3");
            var soma = new Soma(new Numero(2), new Numero(3));
            var substracao = new Subtracao(soma, new Numero(2));
            Console.WriteLine("Resultado: {0}", substracao.Avalia());

            Console.WriteLine("Resolvendo a expresao ((4 * 10) / (1 + 1)) = 20");
            var multiplicacao = new Multiplicacao(new Numero(4), new Numero(10));
            soma = new Soma(new Numero(1), new Numero(1));
            var divisao = new Divisao(multiplicacao, soma);
            Console.WriteLine("Resultado: {0}", divisao.Avalia());

            Console.WriteLine("Resolvendo raiz quadrada de 8 + 10 = 12.8284");
            var raizquadrada = new RaizQuadrada(new Numero(8));
            soma = new Soma(raizquadrada, new Numero(10));
            Console.WriteLine("Resultado: {0}", soma.Avalia());
        }
    }
}
