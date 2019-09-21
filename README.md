# OpenCep

Obtém CEP diretamente dos serviços dos Correios

# Como utilizar

## Objeto de retorno

``` C#
  public class CepResult
    {
        public string Cep { get;  set; }
        public string UF { get;  set; }
        public string Cidade { get;  set; }
        public string Bairro { get;  set; }
        public string Endereco { get;  set; }
        public string Result { get; set; }
        public string Mensagem { get; set; }
    }

```

### Quando ocorrer Error

Neste caso será retornado um `"NOK"` na propriedade `Result` e o detelhe do erro na propriedade `Mensagem`

## Realizando uma consulta

``` C#
Correios _correios = new Correios();

var _result = _correios.Consulta("32000000");

```
