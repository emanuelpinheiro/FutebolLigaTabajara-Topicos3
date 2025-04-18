namespace FutebolLigaTabajara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizacaoMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComissaoTecnicas", "Jogador_JogadorId", c => c.Int());
            AddColumn("dbo.Jogadors", "Jogador_JogadorId", c => c.Int());
            CreateIndex("dbo.ComissaoTecnicas", "Jogador_JogadorId");
            CreateIndex("dbo.Jogadors", "Jogador_JogadorId");
            AddForeignKey("dbo.ComissaoTecnicas", "Jogador_JogadorId", "dbo.Jogadors", "JogadorId");
            AddForeignKey("dbo.Jogadors", "Jogador_JogadorId", "dbo.Jogadors", "JogadorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jogadors", "Jogador_JogadorId", "dbo.Jogadors");
            DropForeignKey("dbo.ComissaoTecnicas", "Jogador_JogadorId", "dbo.Jogadors");
            DropIndex("dbo.Jogadors", new[] { "Jogador_JogadorId" });
            DropIndex("dbo.ComissaoTecnicas", new[] { "Jogador_JogadorId" });
            DropColumn("dbo.Jogadors", "Jogador_JogadorId");
            DropColumn("dbo.ComissaoTecnicas", "Jogador_JogadorId");
        }
    }
}
