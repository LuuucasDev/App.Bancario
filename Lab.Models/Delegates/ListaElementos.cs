using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Models.Delegates
{
    public class ListaElementos<T>
    {
        private T[] elementos;

        public void CriarElementos(T[] elementos)
        {
            this.elementos = elementos;
        }

        public T Buscar(Predicate<T> verifica)
        {
            foreach (var item in elementos)
            {
                if (verifica(item))
                {
                    return item;
                }
            }
            return default(T);
        }

        public IEnumerable<T> Listar(Predicate<T> verifica)
        {
            foreach (var item in elementos)
            {
                if (verifica(item))
                {
                    yield return item;
                }
            }
        }
    }
}
