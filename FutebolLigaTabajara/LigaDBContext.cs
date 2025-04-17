using FutebolLigaTabajara.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FutebolLigaTabajara
{
    public class LigaDBContext : DbContext
    {
        public DbSet<Time> Times { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<ComissaoTecnica> ComissaoTecnica { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Estatistica> Estatisticas { get; set; }

        public LigaDBContext() : base("LigaDBContext") { }
    }

    public static class LigaDBInitializer
    {
        public static void Seed(LigaDBContext context)
        {
            if (!context.Times.Any())
            {
                var times = new List<Time>
                {
                    new Time { Nome = "Tabajara FC", Cidade = "Tabajara City", Estado = "TO", AnoFundacao = 1998 },
                    new Time { Nome = "Ajax do Cerrado", Cidade = "Cerradópolis", Estado = "CE", AnoFundacao = 2002 },
                    new Time { Nome = "Guerreiros da Serra", Cidade = "Serra Alta", Estado = "SP", AnoFundacao = 2005 }
                };

                context.Times.AddRange(times);
                context.SaveChanges();

                var jogadores = new List<Jogador>
                {
                    new Jogador {
                        Nome = "João Canela",
                        DataNascimento = new DateTime(1995, 5, 10),
                        Nacionalidade = "Brasileiro",
                        Posicao = Posicao.Atacante,
                        NumeroCamisa = 9,
                        Altura = 1.80,
                        Peso = 75,
                        PePreferido = PePreferido.Direito,
                        TimeId = times[0].TimeId
                    },
                    new Jogador {
                        Nome = "Carlos Liso",
                        DataNascimento = new DateTime(1998, 3, 22),
                        Nacionalidade = "Brasileiro",
                        Posicao = Posicao.Goleiro,
                        NumeroCamisa = 1,
                        Altura = 1.90,
                        Peso = 82,
                        PePreferido = PePreferido.Esquerdo,
                        TimeId = times[0].TimeId
                    },
                    new Jogador {
                        Nome = "Ramon Correria",
                        DataNascimento = new DateTime(2000, 11, 1),
                        Nacionalidade = "Brasileiro",
                        Posicao = Posicao.Meia,
                        NumeroCamisa = 8,
                        Altura = 1.72,
                        Peso = 68,
                        PePreferido = PePreferido.Direito,
                        TimeId = times[1].TimeId
                    },
                    new Jogador {
                        Nome = "José Barreira",
                        DataNascimento = new DateTime(1992, 7, 15),
                        Nacionalidade = "Brasileiro",
                        Posicao = Posicao.Zagueiro,
                        NumeroCamisa = 4,
                        Altura = 1.85,
                        Peso = 80,
                        PePreferido = PePreferido.Esquerdo,
                        TimeId = times[2].TimeId
                    }
                };

                context.Jogadores.AddRange(jogadores);
                context.SaveChanges();

                var tecnicos = new List<ComissaoTecnica>
                {
                    new ComissaoTecnica { Nome = "Seu Zé", Cargo = Cargo.Treinador, TimeId = times[0].TimeId },
                    new ComissaoTecnica { Nome = "Mestre Pança", Cargo = Cargo.PreparadorFisico, TimeId = times[1].TimeId },
                    new ComissaoTecnica { Nome = "Dona Aurora", Cargo = Cargo.Fisioterapeuta, TimeId = times[2].TimeId }
                };

                context.ComissaoTecnica.AddRange(tecnicos);
                context.SaveChanges();

                var partidas = new List<Partida>
                {
                    new Partida
                    {
                        DataHora = new DateTime(2025, 10, 15, 16, 0, 0),
                        Rodada = 1,
                        Estadio = "Estádio Tabajara",
                        TimeCasaId = times[0].TimeId,
                        TimeForaId = times[1].TimeId,
                        GolsTimeCasa = 2,
                        GolsTimeFora = 1
                    },
                    new Partida
                    {
                        DataHora = new DateTime(2025, 10, 22, 18, 0, 0),
                        Rodada = 2,
                        Estadio = "Arena Cerrado",
                        TimeCasaId = times[1].TimeId,
                        TimeForaId = times[2].TimeId,
                        GolsTimeCasa = 0,
                        GolsTimeFora = 3
                    },
                    new Partida
                    {
                        DataHora = new DateTime(2025, 10, 29, 20, 0, 0),
                        Rodada = 3,
                        Estadio = "Estádio da Serra",
                        TimeCasaId = times[2].TimeId,
                        TimeForaId = times[0].TimeId,
                        GolsTimeCasa = 1,
                        GolsTimeFora = 1
                    }
                };

                context.Partidas.AddRange(partidas);
                context.SaveChanges();

                var estatisticas = new List<Estatistica>
                {
                    new Estatistica
                    {
                        JogadorId = jogadores[0].JogadorId,
                        PartidaId = partidas[0].PartidaId,
                        Gols = 1,
                        Assistencias = 1,
                        Titular = true,
                        MinutosJogador = 90
                    },
                    new Estatistica
                    {
                        JogadorId = jogadores[1].JogadorId,
                        PartidaId = partidas[0].PartidaId,
                        Gols = 0,
                        Assistencias = 0,
                        Titular = true,
                        MinutosJogador = 90
                    },
                    new Estatistica
                    {
                        JogadorId = jogadores[2].JogadorId,
                        PartidaId = partidas[1].PartidaId,
                        Gols = 0,
                        Assistencias = 0,
                        Titular = true,
                        MinutosJogador = 85
                    },
                    new Estatistica
                    {
                        JogadorId = jogadores[3].JogadorId,
                        PartidaId = partidas[2].PartidaId,
                        Gols = 1,
                        Assistencias = 0,
                        Titular = true,
                        MinutosJogador = 90
                    }
                };

                context.Estatisticas.AddRange(estatisticas);
                context.SaveChanges();
            }
        }
    }
}
