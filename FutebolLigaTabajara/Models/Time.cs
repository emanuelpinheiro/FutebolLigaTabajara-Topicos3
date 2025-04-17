using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FutebolLigaTabajara.Models
{
    public class Time
    {
        public int TimeId { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int AnoFundacao { get; set; }
        public string Estadio { get; set; }
        public int CapacidadeEstadio { get; set; }
        public string CorUniformePrimaria { get; set; }
        public string CorUniformeSecundaria { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Jogador> Jogadores { get; set; }
        public virtual ICollection<ComissaoTecnica> ComissaoTecnica { get; set; }

        public bool EstaApto()
        {
            var cargosUnicos = ComissaoTecnica?.Select(c => c.Cargo).Distinct().Count() ?? 0;

            return
                Jogadores?.Count >= 30 &&
                ComissaoTecnica?.Count >= 5 &&
                cargosUnicos == ComissaoTecnica?.Count;
        }
    }
}