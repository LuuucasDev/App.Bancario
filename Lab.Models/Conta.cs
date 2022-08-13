using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Models
{
    public abstract class Conta
    {
        public int NumeroBanco { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }

        public List<Movimento> Movimentos { get; set; }

        public Cliente ClienteInfo { get; set; }

        public Conta(int Banco, string Agencia, string Conta)
        {
            if(this.Movimentos == null)
            {
                this.Movimentos = new List<Movimento>();
            }

            this.NumeroBanco = Banco;
            this.NumeroAgencia = Agencia;
            this.NumeroConta = Conta;
        }

        protected async Task RegistrarMovimento(double valor, Operacoes operacao)
        {
            if (valor <= 0)
            {
                throw new
                    ArgumentException("O valor deve ser positivo");
            }

            this.Movimentos.Add(new Movimento()
            {
                Data = DateTime.Now,
                Historico = operacao == Operacoes.Saque ? "SAQUE" : "DEPÓSITO",
                Operacao = operacao,
                Valor = valor
            }); 
        }

        public abstract string MostrarExtrato();
        
        public abstract void EfetuarOperacao(double valor,
            Operacoes operacao = Operacoes.Deposito);

        public override string ToString()
        {
            return this.NumeroAgencia + "/" + this.NumeroConta;
        }
    }
}
