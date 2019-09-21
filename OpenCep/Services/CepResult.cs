using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCep.Services
{
    public class CepResult
    {
        public CepResult()
        {
            this.Result = "OK";
            this.Mensagem = "Sucesso";
        }

        public string Cep { get;  set; }
        public string UF { get;  set; }
        public string Cidade { get;  set; }
        public string Bairro { get;  set; }
        public string Endereco { get;  set; }
        public string Result { get; private set; }

        private string _Mensagem;
        public string Mensagem
        {
            get
            {
                return _Mensagem;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Result = "OK";
                    _Mensagem = "Sucesso";
                }
                else
                {
                    Result = "NOK";
                    _Mensagem = value;
                }
            }
        }
    }
}
