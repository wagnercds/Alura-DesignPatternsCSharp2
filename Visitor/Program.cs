using System;

namespace Visitor
{
    interface IVisitor
    {
        void ImprimeSoma(Soma soma);
        void ImprimeSubtracao(Subtracao subtracao);
        void ImprimeNumero(Numero numero);
        void ImprimeMultiplicacao(Multiplicacao multiplicacao);
        void ImprimeDivisao(Divisao divisao);
        void ImprimeRaizQuadrada(RaizQuadrada raizQuadrada);
    }
    interface IExpressao
    {
        double Avalia();
        void Aceita(IVisitor impressora);
    }
    class Impressora : IVisitor
    {
        public void ImprimeNumero(Numero numero)
        {
            Console.Write(numero.Valor);
        }
        private void ImprimeExpressao(BaseExpressao baseExpressao, string operador)
        {
            Console.Write("(");
            baseExpressao.Esquerda.Aceita(this);
            Console.Write($" {operador} ");
            baseExpressao.Direita.Aceita(this);
            Console.Write(")");
        }
        public void ImprimeSoma(Soma soma)
        {
            ImprimeExpressao(soma, "+");
        }
        public void ImprimeSubtracao(Subtracao subtracao)
        {
            ImprimeExpressao(subtracao, "-");
        }
        public void ImprimeMultiplicacao(Multiplicacao multiplicacao)
        {
            ImprimeExpressao(multiplicacao, "*");
        }
        public void ImprimeDivisao(Divisao divisao)
        {
            ImprimeExpressao(divisao, "/");
        }
        public void ImprimeRaizQuadrada(RaizQuadrada raizQuadrada)
        {
            Console.Write("√");
            raizQuadrada.Valor.Aceita(this);
        }
    }
    class ImpressoraEsquerda : IVisitor
    {
        public void ImprimeNumero(Numero numero)
        {
            Console.Write(numero.Valor);
        }
        private void ImprimeExpressao(BaseExpressao baseExpressao, string operador)
        {
            Console.Write("(");
            Console.Write($"{operador} ");
            baseExpressao.Esquerda.Aceita(this);
            Console.Write(" ");
            baseExpressao.Direita.Aceita(this);
            Console.Write(")");
        }
        public void ImprimeSoma(Soma soma)
        {
            ImprimeExpressao(soma, "+");
        }
        public void ImprimeSubtracao(Subtracao subtracao)
        {
            ImprimeExpressao(subtracao, "-");
        }
        public void ImprimeMultiplicacao(Multiplicacao multiplicacao)
        {
            ImprimeExpressao(multiplicacao, "*");
        }
        public void ImprimeDivisao(Divisao divisao)
        {
            ImprimeExpressao(divisao, "/");
        }
        public void ImprimeRaizQuadrada(RaizQuadrada raizQuadrada)
        {
            Console.Write("√");
            raizQuadrada.Valor.Aceita(this);
        }
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
        public void Aceita(IVisitor impressora)
        {
            impressora.ImprimeNumero(this);
        }
    }
    abstract class BaseExpressao : IExpressao
    {
        public IExpressao Esquerda { get; private set; }
        public IExpressao Direita { get; private set; }
        public BaseExpressao(IExpressao esquerda, IExpressao direita)
        {
            this.Esquerda = esquerda;
            this.Direita = direita;
        }
        public abstract double Avalia();
        public abstract void Aceita(IVisitor impressora);
    }
    class Soma : BaseExpressao
    {
        public Soma(IExpressao esquerda, IExpressao direita) : base(esquerda, direita) { }
        public override void Aceita(IVisitor impressora)
        {
            impressora.ImprimeSoma(this);
        }
        public override double Avalia()
        {
            return this.Esquerda.Avalia() + this.Direita.Avalia();
        }
    }
    class Subtracao : BaseExpressao
    {
        public Subtracao(IExpressao esquerda, IExpressao direita) : base(esquerda, direita) { }
        public override void Aceita(IVisitor impressora)
        {
            impressora.ImprimeSubtracao(this);
        }
        public override double Avalia()
        {
            return this.Esquerda.Avalia() - this.Direita.Avalia();
        }
    }
    class Multiplicacao : BaseExpressao
    {
        public Multiplicacao(IExpressao esquerda, IExpressao direita) : base(esquerda, direita) { }
        public override void Aceita(IVisitor impressora)
        {
            impressora.ImprimeMultiplicacao(this);
        }
        public override double Avalia()
        {
            return this.Esquerda.Avalia() * this.Direita.Avalia();
        }
    }
    class Divisao : BaseExpressao
    {
        public Divisao(IExpressao esquerda, IExpressao direita) : base(esquerda, direita) {  }
        public override void Aceita(IVisitor impressora)
        {
            impressora.ImprimeDivisao(this);
        }
        public override double Avalia()
        {
            return this.Esquerda.Avalia() / this.Direita.Avalia();
        }
    }
    class RaizQuadrada : IExpressao
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
        public void Aceita(IVisitor impressora)
        {
            impressora.ImprimeRaizQuadrada(this);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var impressora = new ImpressoraEsquerda();
            Console.WriteLine("Resolvendo a expressão (2 + 3) - 2  = 3");
            var soma = new Soma(new Numero(2), new Numero(3));
            var substracao = new Subtracao(soma, new Numero(2));
            substracao.Aceita(impressora);
            Console.WriteLine(" = {0}", substracao.Avalia());

            Console.WriteLine("Resolvendo a expresao ((4 * 10) / (1 + 1)) = 20");
            var multiplicacao = new Multiplicacao(new Numero(4), new Numero(10));
            soma = new Soma(new Numero(1), new Numero(1));
            var divisao = new Divisao(multiplicacao, soma);
            divisao.Aceita(impressora);
            Console.WriteLine(" = {0}", divisao.Avalia());

            Console.WriteLine("Resolvendo raiz quadrada de 8 + 10 = 12.8284");
            var raizquadrada = new RaizQuadrada(new Numero(8));
            soma = new Soma(raizquadrada, new Numero(10));
            soma.Aceita(impressora);
            Console.WriteLine(" = {0}", soma.Avalia());
        }
    }
}
