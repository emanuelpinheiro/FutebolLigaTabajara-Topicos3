using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FutebolLigaTabajara.Models
{
    public class Estatistica
    {
        public int EstatisticaId { get; set; }

        public int JogadorId { get; set; }
        public virtual Jogador Jogador { get; set; }

        public int PartidaId { get; set; }
        public virtual Partida Partida { get; set; }

        public int Gols { get; set; }

        public int Assistencias { get; set; }

        public bool Titular { get; set; }

        public int MinutosJogador { get; set; }
    }

}