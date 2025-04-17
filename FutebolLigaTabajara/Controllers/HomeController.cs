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
            return View(times);
        }
    }

}