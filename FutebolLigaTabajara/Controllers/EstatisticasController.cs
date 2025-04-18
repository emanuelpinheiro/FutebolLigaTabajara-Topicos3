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
    public class EstatisticasController : Controller
    {
        private LigaDBContext db = new LigaDBContext();

        // GET: Estatisticas
        public ActionResult Index()
        {
            var estatisticas = db.Estatisticas.Include(e => e.Jogador).Include(e => e.Partida);
            return View(estatisticas.ToList());
        }
        public ActionResult Artilharia()
        {
            var artilheiros = db.Estatisticas
                .GroupBy(e => e.Jogador)
                .Select(g => new
                {
                    Jogador = g.Key,
                    Gols = g.Sum(e => e.Gols)
                })
                .OrderByDescending(a => a.Gols)
                .Take(10)
                .ToList();

            return View(artilheiros);
        }

        // GET: Estatisticas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatistica estatistica = db.Estatisticas.Find(id);
            if (estatistica == null)
            {
                return HttpNotFound();
            }
            return View(estatistica);
        }

        // GET: Estatisticas/Create
        public ActionResult Create()
        {
            ViewBag.JogadorId = new SelectList(db.Jogadores, "JogadorId", "Nome");
            ViewBag.PartidaId = new SelectList(db.Partidas, "PartidaId", "Estadio");
            return View();
        }

        // POST: Estatisticas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstatisticaId,JogadorId,PartidaId,Gols,Assistencias,Titular,MinutosJogador")] Estatistica estatistica)
        {
            if (ModelState.IsValid)
            {
                db.Estatisticas.Add(estatistica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorId = new SelectList(db.Jogadores, "JogadorId", "Nome", estatistica.JogadorId);
            ViewBag.PartidaId = new SelectList(db.Partidas, "PartidaId", "Estadio", estatistica.PartidaId);
            return View(estatistica);
        }

        // GET: Estatisticas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatistica estatistica = db.Estatisticas.Find(id);
            if (estatistica == null)
            {
                return HttpNotFound();
            }
            ViewBag.JogadorId = new SelectList(db.Jogadores, "JogadorId", "Nome", estatistica.JogadorId);
            ViewBag.PartidaId = new SelectList(db.Partidas, "PartidaId", "Estadio", estatistica.PartidaId);
            return View(estatistica);
        }

        // POST: Estatisticas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstatisticaId,JogadorId,PartidaId,Gols,Assistencias,Titular,MinutosJogador")] Estatistica estatistica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estatistica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JogadorId = new SelectList(db.Jogadores, "JogadorId", "Nome", estatistica.JogadorId);
            ViewBag.PartidaId = new SelectList(db.Partidas, "PartidaId", "Estadio", estatistica.PartidaId);
            return View(estatistica);
        }

        // GET: Estatisticas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatistica estatistica = db.Estatisticas.Find(id);
            if (estatistica == null)
            {
                return HttpNotFound();
            }
            return View(estatistica);
        }

        // POST: Estatisticas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estatistica estatistica = db.Estatisticas.Find(id);
            db.Estatisticas.Remove(estatistica);
            db.SaveChanges();
            return RedirectToAction("Index");
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
