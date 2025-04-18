namespace FutebolLigaTabajara.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FutebolLigaTabajara.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FutebolLigaTabajara.LigaDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FutebolLigaTabajara.LigaDBContext context)
        {
            // Populando a tabela Times
            if (!context.Times.Any())
            {
                var times = new List<Time>();
                for (int i = 1; i <= 20; i++)
                {
                    times.Add(new Time
                    {
                        Nome = $"Time {i}",
                        Cidade = $"Cidade {i}",
                        Estado = "SP",
                        AnoFundacao = 2000 + i,
                        Status = true
                    });
                }

                context.Times.AddOrUpdate(t => t.Nome, times.ToArray());
                context.SaveChanges();

                foreach (var time in times)
                {
                    var jogadores = new List<Jogador>();
                    for (int j = 1; j <= 30; j++)
                    {
                        jogadores.Add(new Jogador
                        {
                            Nome = $"Jogador {j} do {time.Nome}",
                            DataNascimento = new DateTime(1990 + j % 10, 1, 1),
                            Nacionalidade = "Brasileiro",
                            Posicao = Posicao.Atacante,
                            NumeroCamisa = j,
                            Altura = 1.80,
                            Peso = 75,
                            PePreferido = PePreferido.Direito,
                            TimeId = time.TimeId
                        });
                    }

                    context.Jogadores.AddRange(jogadores);

                    var comissao = new List<ComissaoTecnica>
        {
            new ComissaoTecnica { Nome = "Treinador", Cargo = Cargo.Treinador, TimeId = time.TimeId },
            new ComissaoTecnica { Nome = "Preparador Físico", Cargo = Cargo.PreparadorFisico, TimeId = time.TimeId },
            new ComissaoTecnica { Nome = "Fisioterapeuta", Cargo = Cargo.Fisioterapeuta, TimeId = time.TimeId },
            new ComissaoTecnica { Nome = "Auxiliar Técnico", Cargo = Cargo.Auxiliar, TimeId = time.TimeId },
            new ComissaoTecnica { Nome = "Treinador de Goleiros", Cargo = Cargo.TreinadorGoleiros, TimeId = time.TimeId }
        };

                    context.ComissaoTecnica.AddRange(comissao);
                }

                context.SaveChanges();
            }


            // Populando a tabela ComissaoTecnica
            if (!context.ComissaoTecnica.Any())
            {
                var times = context.Times.ToList();

                var comissaoTecnica = new List<ComissaoTecnica>
                {
                    new ComissaoTecnica { Nome = "Seu Zé", Cargo = Cargo.Treinador, DataNascimento = new DateTime(1970, 5, 15), TimeId = times[0].TimeId },
                    new ComissaoTecnica { Nome = "Mestre Pança", Cargo = Cargo.PreparadorFisico, DataNascimento = new DateTime(1980, 3, 20), TimeId = times[1].TimeId },
                    new ComissaoTecnica { Nome = "Dona Aurora", Cargo = Cargo.Fisioterapeuta, DataNascimento = new DateTime(1985, 7, 10), TimeId = times[2].TimeId }
                };

                context.ComissaoTecnica.AddOrUpdate(c => c.Nome, comissaoTecnica.ToArray());
                context.SaveChanges();
            }

            // Populando a tabela Jogadores
            if (!context.Jogadores.Any())
            {
                var times = context.Times.ToList();

                var jogadores = new List<Jogador>
                {
                    new Jogador { Nome = "João Canela", DataNascimento = new DateTime(1995, 5, 10), Nacionalidade = "Brasileiro", Posicao = Posicao.Atacante, NumeroCamisa = 9, Altura = 1.80, Peso = 75, PePreferido = PePreferido.Direito, TimeId = times[0].TimeId },
                    new Jogador { Nome = "Carlos Liso", DataNascimento = new DateTime(1998, 3, 22), Nacionalidade = "Brasileiro", Posicao = Posicao.Goleiro, NumeroCamisa = 1, Altura = 1.90, Peso = 82, PePreferido = PePreferido.Esquerdo, TimeId = times[0].TimeId },
                    new Jogador { Nome = "Ramon Correria", DataNascimento = new DateTime(2000, 11, 1), Nacionalidade = "Brasileiro", Posicao = Posicao.Meia, NumeroCamisa = 8, Altura = 1.72, Peso = 68, PePreferido = PePreferido.Direito, TimeId = times[1].TimeId },
                    new Jogador { Nome = "José Barreira", DataNascimento = new DateTime(1992, 7, 15), Nacionalidade = "Brasileiro", Posicao = Posicao.Zagueiro, NumeroCamisa = 4, Altura = 1.85, Peso = 80, PePreferido = PePreferido.Esquerdo, TimeId = times[2].TimeId }
                };

                context.Jogadores.AddOrUpdate(j => j.Nome, jogadores.ToArray());
                context.SaveChanges();
            }

            // Populando a tabela Partidas
            if (!context.Partidas.Any())
            {
                var times = context.Times.ToList();

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

                context.Partidas.AddOrUpdate(p => new { p.DataHora, p.TimeCasaId, p.TimeForaId }, partidas.ToArray());
                context.SaveChanges();
            }

            // Populando a tabela Estatísticas
            if (!context.Estatisticas.Any())
            {
                var jogadores = context.Jogadores.ToList();
                var partidas = context.Partidas.ToList();

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

                context.Estatisticas.AddOrUpdate(e => new { e.JogadorId, e.PartidaId }, estatisticas.ToArray());
                context.SaveChanges();
            }
        }
    }
}
