using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FutebolLigaTabajara.Models
{
    public class Partida
    {
        public int PartidaId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        public int Rodada { get; set; }

        public int TimeCasaId { get; set; }
        public virtual Time TimeCasa { get; set; }

        public int TimeForaId { get; set; }
        public virtual Time TimeFora { get; set; }

        public int GolsTimeCasa { get; set; }

        public int GolsTimeFora { get; set; }

        public string Estadio { get; set; }

        public virtual ICollection<Estatistica> Estatisticas { get; set; }
    }

}