using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Models
{
    public class ContaCorrente : Conta
    {
        public double Saldo { get; protected set; }
        //public Cliente ClienteInfo { get; set; }

        public ContaCorrente(int Banco, string Agencia, string Conta): 
            base(Banco, Agencia, Conta)
        { }

        public ContaCorrente(int Banco, string Agencia, string Conta, 
            Cliente cliente)
            : this(Banco, Agencia, Conta)
        {
            this.ClienteInfo = cliente;
        }

        public async override void EfetuarOperacao(double valor, 
            Operacoes operacao = Operacoes.Deposito)
        {
            if(valor <= 0)
            {
                throw new 
                    ArgumentException("O valor deve ser positivo");
            }



            switch (operacao)
            {
                case Operacoes.Deposito:
                    this.Saldo += valor;
                    break;
                case Operacoes.Saque:

                    if (valor > this.Saldo)
                    {
                        throw new
                            InvalidOperationException("Saldo insuficiente");
                    }
                    this.Saldo -= valor;
                    break;
            }
            await base.RegistrarMovimento(valor, operacao);
        }

        public virtual string Exibir()
        {
            string cliente = this.ClienteInfo != null ? 
                this.ClienteInfo.Exibir() + '\n' : "";

            return $"{cliente}" + 
                $"Banco: {this.NumeroBanco}\n" +
                $"Agência: {this.NumeroAgencia}\n" +
                $"Conta: {this.NumeroConta}\n" +
                $"Saldo Atual: {this.Saldo}";
        }

        public override string MostrarExtrato()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Cliente: {ClienteInfo.Nome}\n")
                .Append($"Banco: {NumeroBanco}\n")
                .Append($"Agência: {NumeroAgencia}\n")
                .Append($"Conta: {NumeroConta}\n")
                .Append(new string('-', 35) + '\n');

            if(this.Movimentos.Count() == 0)
            {
                builder.Append("Nenhum movimento registrado para esta conta");
            }
            else
            {
                foreach (var item in this.Movimentos)
                {
                    builder.Append($"{item}\n");
                }
            }
            builder.Append(new string('-', 35) + '\n');
            builder.Append($"Saldo: {this.Saldo:c}");

            return builder.ToString();
        }
    }
}
