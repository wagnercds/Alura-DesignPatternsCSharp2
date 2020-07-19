using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace Adapter
{
    public class GeradorXML<T>
    {
        public string XML(T objeto)
        {
            var serializador = new XmlSerializer(objeto.GetType());
            var stringWriter = new StringWriter();
            serializador.Serialize(stringWriter, objeto);
            var result = stringWriter.ToString();
            return result;
        }
    }

    public class Cliente
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }

        public Cliente()
        {

        }

        public Cliente(string nome, string endereco, string telefone)
        {
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var cliente = new Cliente("teste nome", "teste endereco", "11111-11111");
            var geradorXML = new GeradorXML<Cliente>();
            string xml = geradorXML.XML(cliente);
            Console.WriteLine("XML: {0}", xml);
        }
    }
}
