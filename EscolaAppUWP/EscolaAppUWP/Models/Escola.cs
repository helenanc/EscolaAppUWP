using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaAppUWP.Models
{
    class Escola
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public double CienciasNatureza { get; set; }
        public double CienciasHumanas { get; set; }
        public double LinguagensCodigos { get; set; }
        public double Matematica { get; set; }
        public double Redacao { get; set; }
    }
}
