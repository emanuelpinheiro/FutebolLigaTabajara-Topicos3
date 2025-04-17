namespace FutebolLigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComissaoTecnicas",
                c => new
                    {
                        ComissaoTecnicaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Cargo = c.Int(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        TimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComissaoTecnicaId)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: true)
                .Index(t => t.TimeId);
            
            CreateTable(
                "dbo.Times",
                c => new
                    {
                        TimeId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        AnoFundacao = c.Int(nullable: false),
                        Estadio = c.String(),
                        CapacidadeEstadio = c.Int(nullable: false),
                        CorUniformePrimaria = c.String(),
                        CorUniformeSecundaria = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TimeId);
            
            CreateTable(
                "dbo.Jogadors",
                c => new
                    {
                        JogadorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Nacionalidade = c.String(),
                        Posicao = c.Int(nullable: false),
                        NumeroCamisa = c.Int(nullable: false),
                        Altura = c.Double(nullable: false),
                        Peso = c.Double(nullable: false),
                        PePreferido = c.Int(nullable: false),
                        TimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JogadorId)
                .ForeignKey("dbo.Times", t => t.TimeId, cascadeDelete: true)
                .Index(t => t.TimeId);
            
            CreateTable(
                "dbo.Estatisticas",
                c => new
                    {
                        EstatisticaId = c.Int(nullable: false, identity: true),
                        JogadorId = c.Int(nullable: false),
                        PartidaId = c.Int(nullable: false),
                        Gols = c.Int(nullable: false),
                        Assistencias = c.Int(nullable: false),
                        Titular = c.Boolean(nullable: false),
                        MinutosJogador = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstatisticaId)
                .ForeignKey("dbo.Jogadors", t => t.JogadorId, cascadeDelete: true)
                .ForeignKey("dbo.Partidas", t => t.PartidaId, cascadeDelete: true)
                .Index(t => t.JogadorId)
                .Index(t => t.PartidaId);
            
            CreateTable(
                "dbo.Partidas",
                c => new
                    {
                        PartidaId = c.Int(nullable: false, identity: true),
                        DataHora = c.DateTime(nullable: false),
                        Rodada = c.Int(nullable: false),
                        TimeCasaId = c.Int(nullable: false),
                        TimeForaId = c.Int(nullable: false),
                        GolsTimeCasa = c.Int(nullable: false),
                        GolsTimeFora = c.Int(nullable: false),
                        Estadio = c.String(),
                        TimeCasa_TimeId = c.Int(),
                        TimeFora_TimeId = c.Int(),
                    })
                .PrimaryKey(t => t.PartidaId)
                .ForeignKey("dbo.Times", t => t.TimeCasa_TimeId)
                .ForeignKey("dbo.Times", t => t.TimeFora_TimeId)
                .Index(t => t.TimeCasa_TimeId)
                .Index(t => t.TimeFora_TimeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partidas", "TimeFora_TimeId", "dbo.Times");
            DropForeignKey("dbo.Partidas", "TimeCasa_TimeId", "dbo.Times");
            DropForeignKey("dbo.Estatisticas", "PartidaId", "dbo.Partidas");
            DropForeignKey("dbo.Estatisticas", "JogadorId", "dbo.Jogadors");
            DropForeignKey("dbo.Jogadors", "TimeId", "dbo.Times");
            DropForeignKey("dbo.ComissaoTecnicas", "TimeId", "dbo.Times");
            DropIndex("dbo.Partidas", new[] { "TimeFora_TimeId" });
            DropIndex("dbo.Partidas", new[] { "TimeCasa_TimeId" });
            DropIndex("dbo.Estatisticas", new[] { "PartidaId" });
            DropIndex("dbo.Estatisticas", new[] { "JogadorId" });
            DropIndex("dbo.Jogadors", new[] { "TimeId" });
            DropIndex("dbo.ComissaoTecnicas", new[] { "TimeId" });
            DropTable("dbo.Partidas");
            DropTable("dbo.Estatisticas");
            DropTable("dbo.Jogadors");
            DropTable("dbo.Times");
            DropTable("dbo.ComissaoTecnicas");
        }
    }
}
