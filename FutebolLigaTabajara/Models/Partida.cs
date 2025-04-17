using FutebolLigaTabajara.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
}
