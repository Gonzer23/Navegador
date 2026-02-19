using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navegador
{
    internal class Direccion
    {
        string url;
        int veces;
        DateTime fechaAccesso;

        public string Url { get => url; set => url = value; }
        public int Veces { get => veces; set => veces = value; }
        public DateTime FechaAccesso { get => fechaAccesso; set => fechaAccesso = value; }
    }

}
