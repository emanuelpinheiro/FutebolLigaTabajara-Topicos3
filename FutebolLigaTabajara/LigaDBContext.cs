using FutebolLigaTabajara.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FutebolLigaTabajara
{
    public class LigaDBContext : DbContext
    {
        public DbSet<Time> Times { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<ComissaoTecnica> ComissaoTecnica { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Estatistica> Estatisticas { get; set; }

        public LigaDBContext() : base("DefaultConnection") { }
    }

}