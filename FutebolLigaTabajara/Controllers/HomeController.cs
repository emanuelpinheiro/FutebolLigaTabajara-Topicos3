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
            var times = db.Times.Include("Jogadores").Include("ComissaoTecnica").ToList();
            var statusLiga = times.Count == 20 && times.All(t => t.Status && t.EstaApto());

            ViewBag.LigaStatus = statusLiga ? "Apta para iniciar" : "Não apta para iniciar";
            ViewBag.TotalTimes = times.Count;
            ViewBag.TotalJogadores = db.Jogadores.Count();
            ViewBag.TotalPartidas = db.Partidas.Count();

            return View(times);
        }

        public ActionResult Resumo()
        {
            var times = db.Times.ToList();
            var partidas = db.Partidas.ToList();

            var classificacao = times.Select(t => new
            {
                Time = t,
                Pontos = partidas.Where(p => p.TimeCasaId == t.TimeId).Sum(p => p.PontosTimeCasa) +
                         partidas.Where(p => p.TimeForaId == t.TimeId).Sum(p => p.PontosTimeFora)
            })
            .OrderByDescending(c => c.Pontos)
            .FirstOrDefault();

            ViewBag.Campeao = classificacao?.Time.Nome ?? "Nenhum";
            return View();
        }

    }

}