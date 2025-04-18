using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutebolLigaTabajara.Models
{
    public class Partida
    {
        public int PartidaId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data e Hora")]
        public DateTime DataHora { get; set; }

        [Display(Name = "Rodada")]
        public int Rodada { get; set; }

        [Display(Name = "Estádio")]
        public string Estadio { get; set; }

        // Times envolvidos na partida
        [Display(Name = "Time da Casa")]
        public int TimeCasaId { get; set; }
        public virtual Time TimeCasa { get; set; }

        [Display(Name = "Time Visitante")]
        public int TimeForaId { get; set; }
        public virtual Time TimeFora { get; set; }

        // Placar
        [Display(Name = "Gols Time da Casa")]
        public int GolsTimeCasa { get; set; }

        [Display(Name = "Gols Time Visitante")]
        public int GolsTimeFora { get; set; }

        // Estatísticas de jogadores por partida
        public virtual ICollection<Estatistica> Estatisticas { get; set; }

        // Resultado da partida (só leitura)
        [Display(Name = "Resultado")]
        public string Resultado
        {
            get
            {
                if (GolsTimeCasa > GolsTimeFora)
                    return $"{TimeCasa?.Nome ?? "Time da Casa"} venceu";
                else if (GolsTimeCasa < GolsTimeFora)
                    return $"{TimeFora?.Nome ?? "Time Visitante"} venceu";
                else
                    return "Empate";
            }
        }

        // Pontos para cada time
        public int PontosTimeCasa => GolsTimeCasa > GolsTimeFora ? 3 : GolsTimeCasa == GolsTimeFora ? 1 : 0;
        public int PontosTimeFora => GolsTimeFora > GolsTimeCasa ? 3 : GolsTimeCasa == GolsTimeFora ? 1 : 0;

        // Saldo de gols por time
        public int SaldoGolsTimeCasa => GolsTimeCasa - GolsTimeFora;
        public int SaldoGolsTimeFora => GolsTimeFora - GolsTimeCasa;

        // Verifica se a partida está apta para iniciar
        public bool EstaApta()
        {
            // Verifica se os times estão definidos e ativos
            if (TimeCasa == null || !TimeCasa.Status || TimeFora == null || !TimeFora.Status)
                return false;

            // Verifica se os times possuem jogadores suficientes
            if (TimeCasa.Jogadores == null || TimeCasa.Jogadores.Count < 11 ||
                TimeFora.Jogadores == null || TimeFora.Jogadores.Count < 11)
                return false;

            // Verifica se os times possuem comissão técnica suficiente
            if (TimeCasa.ComissaoTecnica == null || TimeCasa.ComissaoTecnica.Count < 5 ||
                TimeFora.ComissaoTecnica == null || TimeFora.ComissaoTecnica.Count < 5)
                return false;

            // Verifica se o estádio está definido
            if (string.IsNullOrEmpty(Estadio))
                return false;

            // Verifica se a data e hora da partida é futura
            if (DataHora <= DateTime.Now)
                return false;

            return true;
        }
    }
}