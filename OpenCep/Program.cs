using OpenCep.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCep
{
    class Program
    {
        static void Main(string[] args)
        {
            Correios _c = new Correios();

            Console.Write("Digite Cep: ");

            var _key = Console.ReadLine();

            var _result = _c.Consulta(_key);

            Console.WriteLine(_result.Mensagem); 

            Console.Read();

        }
    }
}
