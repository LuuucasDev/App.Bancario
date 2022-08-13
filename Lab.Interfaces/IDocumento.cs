using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Interfaces
{
    public interface IDocumento
    {
        string NumeroDocumento { get; set; }

        string MostrarDocumento();
    }
}
