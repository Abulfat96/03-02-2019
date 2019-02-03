using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _030219.Models;

namespace _030219.Controllers
{
    public class HomeController : Controller
    {
        private readonly AjaxContext _context = new AjaxContext();

        public ActionResult Index()
        {
            return View(_context.Brends.ToList());
        }

        public ActionResult GetModels(int BrendId)
        {
            Brend brend = _context.Brends.Find(BrendId);
            if (brend == null)
            {
                return HttpNotFound();
            }

            var models = _context.Models.Where(m => m.BrendId == brend.Id).ToList();

            string r = "";

            foreach (var item in models)
            {
                r += "<option value='"+item.Id+"'>" + item.Name + "</option>";
            }

            return Content(r);
        }

        public JsonResult GetModelsJr(int? BrendId)
        {
            if (BrendId == null)
            {
                Response.StatusCode = 400;
                return Json(new
                {
                    message = "BrendId yoxdu"
                }, JsonRequestBehavior.AllowGet);
            }

            Brend brend = _context.Brends.Find(BrendId);

            if (brend == null)
            {
                Response.StatusCode = 404;
                return Json(new {
                    message = "Brend Tapilmadi"
                }, JsonRequestBehavior.AllowGet);
            }

            var models = _context.Models.Where(m => m.BrendId == brend.Id).ToList();

            return Json(models.Select(m => new
            {
                m.Id,
                m.Name
            }), JsonRequestBehavior.AllowGet);
        }
    }
}