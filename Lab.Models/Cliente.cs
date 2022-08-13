using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Interfaces;
using Lab.Utilitarios;

namespace Lab.Models
{
    public abstract class Cliente
    {
        public string Nome { get; set; }

        private int _idade;
        public int Idade 
        { 
            get => _idade; 
            set => _idade = (value >= 0 ? value : 
                throw new InvalidOperationException("A idade não pode ser negativa!")); 
        }
        public Sexos Sexo { get; set; }
        public Endereco EnderecoResidencial { get; set; }

        public List<Conta> Contas { get; set; }

        public Cliente(string Nome, Sexos Sexo)
        {
            if(this.Contas == null)
            {
                this.Contas = new List<Conta>();
            }

            this.Nome = Nome;
            this.Sexo = Sexo;
        }

        public Cliente(string Nome, Sexos Sexo, int idade)
            : this(Nome, Sexo)
        {
            this.Idade = idade;
        }

        public Cliente(string Nome, Sexos Sexo, Endereco endereco)
            : this(Nome, Sexo)
        {

            this.EnderecoResidencial = endereco;
        }

        ~Cliente()
        {
            if(this.Contas != null)
            {
                this.Contas = null;
            }
        }

        public virtual string Exibir()
        {
            return $"Nome: {this.Nome}\n" +
                $"Idade: {this.Idade}\n" +
                $"Sexo: {this.Sexo}\n" +
                $"ENDEREÇO DO CLIENTE:\n" +
                $"{this.EnderecoResidencial.Exibir()}";
        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}
