using System;

namespace Bridge
{
    interface IMensagem
    {
        IEnviador Enviador { get; set; }
        void Envia();
        string Formata();
    }

    interface IEnviador
    {
        void EnviaMensagem(IMensagem mensagem);
    }

    class EnviaPorEmail : IEnviador
    {
        public void EnviaMensagem(IMensagem mensagem)
        {
            Console.WriteLine("Enviando por e-mail");
            Console.WriteLine(mensagem.Formata());
            Console.WriteLine();
        }
    }

    class EnviaPorSMS : IEnviador
    {
        public void EnviaMensagem(IMensagem mensagem)
        {
            Console.WriteLine("Enviando por SMS");
            Console.WriteLine(mensagem.Formata());
            Console.WriteLine();
        }
    }

    class MensagemAdm : IMensagem
    {
        public IEnviador Enviador { get; set; }

        public MensagemAdm() { }

        public MensagemAdm(IEnviador enviador)
        {
            Enviador = enviador;
        }

        public void Envia()
        {
            Enviador.EnviaMensagem(this);
        }

        public string Formata()
        {
            return "Mensagem administrativa";
        }
    }

    class MensagemCliente : IMensagem
    {
        public IEnviador Enviador { get; set; }

        private string _nome;

        public MensagemCliente() { }

        public MensagemCliente(IEnviador enviador)
        {
            Enviador = enviador;
        }

        public void Envia()
        {
            Enviador.EnviaMensagem(this);
        }

        public void SetaCliente(string nome)
        {
            _nome = nome;
        }

        public string Formata()
        {
            return $"Mensagem p/ Cliente: {_nome}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var enviaPorEmail = new EnviaPorEmail();
            var enviaPorSMS = new EnviaPorSMS();
            var msgCliente = new MensagemCliente();
            var msgAdm = new MensagemAdm();

            msgAdm.Enviador = enviaPorSMS;
            msgAdm.Envia();

            msgAdm.Enviador = enviaPorEmail;
            msgAdm.Envia();

            msgCliente.Enviador = enviaPorSMS;
            msgCliente.SetaCliente("Nome Cliente");
            msgCliente.Envia();

        }
    }
}
