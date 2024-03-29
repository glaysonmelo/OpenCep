﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace OpenCep.Services
{
    public class Correios
    {
        const string url = "https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente";

        public CepResult ConsultaCep(string cep)
        {
            CepResult _result = new CepResult();

            if (string.IsNullOrWhiteSpace(cep))
            {
                _result.Mensagem = "Cep não informado";
                return _result;
            }

            if (cep.Length < 8)
            {
                _result.Mensagem = "Cep deve conter 8 caracteres";
                return _result;
            }

            if (cep.Length < 8)
            {
                _result.Mensagem = "Cep possui mais de 8 caracteres";
                return _result;
            }

            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "text/xml;charset=UTF-8";

                string xml = $"<?xml version = '1.0' ?>\n<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:cli='http://cliente.bean.master.sigep.bsb.correios.com.br/'>\n<soapenv:Header />\n<soapenv:Body>\n<cli:consultaCEP>\n<cep>${cep}</cep>\n</cli:consultaCEP>\n</soapenv:Body>\n</soapenv:Envelope>";

                xml = xml.Replace("'", "\"");

                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(xml);

                request.ContentLength = bytes.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();

                    _result = ParseSuccessXML(responseStr);
                }
                else
                {
                    _result.Mensagem = $"O seguinte erro ocorreu : {response.StatusCode}";
                }

            }
            catch (WebException e)
            {
                _result.Mensagem = $"O seguinte erro ocorreu : ({e.Status}) {e.Message} ";
            }
            catch (Exception ex)
            {
                _result.Mensagem = ex.Message;
            }

            return _result;
        }

        private CepResult ParseSuccessXML(string xml)
        {
            CepResult _CEP = new CepResult();

            try
            {
                XDocument result = XDocument.Parse(xml);

                _CEP.Cep = result.Descendants("cep").First().Value;
                _CEP.UF = result.Descendants("uf").First().Value;
                _CEP.Cidade = result.Descendants("cidade").First().Value;
                _CEP.Bairro = result.Descendants("bairro").First().Value;
                _CEP.Endereco = result.Descendants("end").First().Value;
            }
            catch (Exception)
            {
                _CEP.Mensagem = "Não foi possível interpretar XML de resposta!";
            }


            return _CEP;
        }

    }
}
