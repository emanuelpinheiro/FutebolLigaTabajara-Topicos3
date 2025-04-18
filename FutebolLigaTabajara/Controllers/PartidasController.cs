using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FutebolLigaTabajara;
using FutebolLigaTabajara.Models;

namespace FutebolLigaTabajara.Controllers
{
    public class PartidasController : Controller
    {
        private LigaDBContext db = new LigaDBContext();

        // GET: Partidas
        public ActionResult Index()
        {
            var partidas = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeFora)
                .ToList();
            return View(partidas);
        }
        public ActionResult GerarRodadas()
        {
            var times = db.Times.ToList();
            var rodadas = new List<Partida>();

            for (int i = 0; i < times.Count - 1; i++)
            {
                for (int j = i + 1; j < times.Count; j++)
                {
                    rodadas.Add(new Partida
                    {
                        TimeCasaId = times[i].TimeId,
                        TimeForaId = times[j].TimeId,
                        DataHora = DateTime.Now.AddDays(rodadas.Count),
                        Rodada = rodadas.Count / (times.Count / 2) + 1,
                        Estadio = times[i].Estadio
                    });

                    rodadas.Add(new Partida
                    {
                        TimeCasaId = times[j].TimeId,
                        TimeForaId = times[i].TimeId,
                        DataHora = DateTime.Now.AddDays(rodadas.Count),
                        Rodada = rodadas.Count / (times.Count / 2) + 1,
                        Estadio = times[j].Estadio
                    });
                }
            }

            db.Partidas.AddRange(rodadas);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Partidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partida partida = db.Partidas.Find(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            return View(partida);
        }

        // GET: Partidas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partidas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartidaId,DataHora,Rodada,Estadio,TimeCasaId,TimeForaId,GolsTimeCasa,GolsTimeFora")] Partida partida)
        {
            if (ModelState.IsValid)
            {
                partida.TimeCasa = db.Times.Find(partida.TimeCasaId);
                partida.TimeFora = db.Times.Find(partida.TimeForaId);

                if (!partida.EstaApta())
                {
                    ModelState.AddModelError("", "A partida não está apta para iniciar. Verifique os critérios.");
                    return View(partida);
                }

                db.Partidas.Add(partida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partida);
        }

        // GET: Partidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partida partida = db.Partidas.Find(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            return View(partida);
        }

        // POST: Partidas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartidaId,DataHora,Rodada,Estadio,TimeCasaId,TimeForaId,GolsTimeCasa,GolsTimeFora")] Partida partida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partida);
        }

        // GET: Partidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partida partida = db.Partidas.Find(id);
            if (partida == null)
            {
                return HttpNotFound();
            }
            return View(partida);
        }

        // POST: Partidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partida partida = db.Partidas.Find(id);
            db.Partidas.Remove(partida);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Classificacao()
        {
            var times = db.Times.ToList();
            var partidas = db.Partidas.ToList();

            var classificacao = times.Select(t => new
            {
                Time = t,
                Pontos = partidas.Where(p => p.TimeCasaId == t.TimeId).Sum(p => p.PontosTimeCasa) +
                         partidas.Where(p => p.TimeForaId == t.TimeId).Sum(p => p.PontosTimeFora),
                SaldoGols = partidas.Where(p => p.TimeCasaId == t.TimeId).Sum(p => p.SaldoGolsTimeCasa) +
                            partidas.Where(p => p.TimeForaId == t.TimeId).Sum(p => p.SaldoGolsTimeFora),
                GolsMarcados = partidas.Where(p => p.TimeCasaId == t.TimeId).Sum(p => p.GolsTimeCasa) +
                               partidas.Where(p => p.TimeForaId == t.TimeId).Sum(p => p.GolsTimeFora)
            })
            .OrderByDescending(c => c.Pontos)
            .ThenByDescending(c => c.SaldoGols)
            .ThenByDescending(c => c.GolsMarcados)
            .ToList();

            return View(classificacao);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
