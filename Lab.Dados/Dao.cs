using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Dados
{
    public abstract class Dao<T, K> where T: class
    {
        public abstract int Incluir(T item);
        public abstract T Buscar(K chave);
        public abstract IEnumerable<T> Listar(K chave = default(K));
        public abstract int Remover(T item);
    }
}
