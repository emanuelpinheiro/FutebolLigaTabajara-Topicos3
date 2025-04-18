using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FutebolLigaTabajara.Controllers
{
    public class HomeController : Controller
    {
        private LigaDBContext db = new LigaDBContext();

        public ActionResult Index()
        {
            var liga = new Liga
            {
                Times = db.Times.Include("Jogadores").Include("ComissaoTecnica").ToList()
            };

            ViewBag.LigaStatus = liga.PodeIniciar() ? "Apta para iniciar" : "Não apta para iniciar";
            ViewBag.TotalTimes = liga.Times.Count;
            ViewBag.TotalJogadores = db.Jogadores.Count();
            ViewBag.TotalPartidas = db.Partidas.Count();

            return View(liga.Times);
        }

        public ActionResult Resumo()
        {
            var times = db.Times.ToList();
            var partidas = db.Partidas.ToList();

            var classificacao = times.Select(t => new
            {
                Time = t,
                Pontos = partidas.Where(p => p.TimeCasaId == t.TimeId).Sum(p => p.PontosTimeCasa) +
                         partidas.Where(p => p.TimeForaId == t.TimeId).Sum(p => p.PontosTimeFora),
                SaldoGols = partidas.Where(p => p.TimeCasaId == t.TimeId).Sum(p => p.SaldoGolsTimeCasa) +
                            partidas.Where(p => p.TimeForaId == t.TimeId).Sum(p => p.SaldoGolsTimeFora)
            })
            .OrderByDescending(c => c.Pontos)
            .ThenByDescending(c => c.SaldoGols)
            .FirstOrDefault();

            ViewBag.Campeao = classificacao?.Time.Nome ?? "Nenhum";
            ViewBag.Pontos = classificacao?.Pontos ?? 0;

            return View();
        }


    }

}