using GestionalePMunicMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionalePMunicMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnagraficaView()
        {
            return View(Anagrafica.AnagraficaList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Anagrafica a)
        {
            Anagrafica.NuovaAnagrafica(a);
            return RedirectToAction("AnagraficaView");
        }

        public ActionResult ViolazioniView()
        {
            return View(Violazione.ViolazioneList());
        }

        public ActionResult CreateVerbale()
        {
            ViewBag.ListaAnagrafica = Anagrafica.AnagraficaListView();

            ViewBag.ListaViolazioni = Violazione.ViolazioneListView();
            return View();
        }

        [HttpPost]
        public ActionResult CreateVerbale(Verbale v)
        {
          Verbale.NuovoVerbale(v);
          return RedirectToAction("AnagraficaView");
        }

        public ActionResult DettagliAnagrafica(int id)
        {
            return View(Anagrafica.GetAnagrafica(id));
        }

        //public ActionResult DettagliAnagraficaViolazioni(int id)
        //{
        //    return View(Anagrafica.GetAnagraficaViolazioni(id));
        //}

        public ActionResult _ListAnagraficheTrasgressori()
        {
            List<Anagrafica> ListAnagrafica = Anagrafica.AnagraficaList();
            return PartialView("_ListAnagraficheTrasgressori", ListAnagrafica);
        }

        public ActionResult _ListTrasgressoriMaggioriDi10Punti()
        {
           List<Anagrafica> lVerbale = Anagrafica.ListVerbaliMaggiori10Punti();
            return PartialView("_ListTrasgressoriMaggioriDi10Punti", lVerbale);
        }

        public ActionResult _ListTrasgressoriMaggioriDi400Euro()
        {
            List<Anagrafica> ListAnagrafica = Anagrafica._ListTrasgressoriMaggioriDi400Euro();
            return PartialView("_ListTrasgressoriMaggioriDi400Euro", ListAnagrafica);
        }

        public ActionResult SearchView(string SearchString)
        {
            
            return View(Anagrafica.SearchMode(SearchString));
        }

        public ActionResult CreateViolazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateViolazione(Violazione v)
        {
            Violazione.NuovaViolazione(v);
            return RedirectToAction("ViolazioniView");
        }
    }
}