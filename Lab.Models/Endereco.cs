using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Models
{
    public struct Endereco
    {
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }

        public Endereco(string Logradouro, int Numero, string Cidade, string Cep)
        {
            this.Logradouro = Logradouro;
            this.Numero = Numero;
            this.Cidade = Cidade;
            this.Cep = Cep;
        }

        public string Exibir()
        {
            return $"Logradouro: {this.Logradouro}\n" +
                $"Número: {this.Numero}\n" +
                $"Cidade: {this.Cidade}\n" +
                $"CEP: {this.Cep}";
        }
    }
}
