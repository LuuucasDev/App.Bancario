using Lab.Models;
using System;
using System.Collections.Generic;

namespace Lab.Dados
{
    public class ContasDao : Dao<Conta, string>
    {
        public override Conta Buscar(string chave)
        {
            throw new NotImplementedException();
        }

        public override int Incluir(Conta item)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Conta> Listar(string chave = null)
        {
            throw new NotImplementedException();
        }

        public override int Remover(Conta item)
        {
            throw new NotImplementedException();
        }
    }
}
