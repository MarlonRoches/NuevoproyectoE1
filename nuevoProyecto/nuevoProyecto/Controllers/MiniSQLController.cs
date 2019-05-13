using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nuevoProyecto.Data;
using nuevoProyecto.Models;

namespace nuevoProyecto.Controllers
{
    public class MiniSQLController : Controller
    {
        // GET: MiniSQL
        public ActionResult PalabrasReservadas()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PalabrasReservadas(FormCollection collection)
        {
            try
            {
                //Enviar a singletone
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MiniSQL/Create
        public ActionResult Consola()
        {
            return View();
        }

        // POST: MiniSQL/Create
        [HttpPost]
        public ActionResult Consola(FormCollection collection)
        {
            Singleton.Instance.Palabras_Reservadas();
            try
            {
                //Enviar a singletone
                Singleton.Instance.Input(collection["Data"]);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
    }
}
