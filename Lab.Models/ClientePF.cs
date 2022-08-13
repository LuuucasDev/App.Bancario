using Lab.Interfaces;
using Lab.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Models
{
    public class ClientePF : Cliente, IDocumento
    {
        //propriedade implementada da interface
        private string _cpf;
        public string NumeroDocumento 
        { 
            get => _cpf; 
            set => _cpf = (value.ValidarCPF() ? value : 
                throw new Exception("CPF Inválido")); 
        }

        //método implementado da interface
        public string MostrarDocumento()
        {
            return $"CPF do cliente: {NumeroDocumento}";
        }

        //construtores
        public ClientePF(string Cpf, string Nome, Sexos Sexo)
            : base(Nome, Sexo)
        {
            this.NumeroDocumento = Cpf;
        }

        public ClientePF(string Cpf, string Nome, Sexos Sexo, 
            int Idade, Endereco endereco)
              : base(Nome, Sexo, Idade)
        {
            this.NumeroDocumento = Cpf;
            base.EnderecoResidencial = endereco;
        }

        public override string Exibir()
        {
            return $"{MostrarDocumento()}\n" +
                $"{base.Exibir()}" ;
        }
    }
}
