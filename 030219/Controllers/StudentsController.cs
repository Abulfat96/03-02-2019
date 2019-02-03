using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using _030219.Models;

namespace _030219.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AjaxContext _context = new AjaxContext();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            var data = _context.Students.Include("Group").OrderBy(s => s.Name).Select(s => new
            {
                s.Id,
                s.Name,
                s.Surname,
                Group = s.Group.Name
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Groups()
        {
            var data = _context.Groups.Select(g => new
            {
                g.Id,
                g.Name
            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;

                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

                return Json(errorList, JsonRequestBehavior.AllowGet);
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            student.Group = _context.Groups.Find(student.GroupId);

            return Json(new {
                student.Id,
                student.Name,
                student.Surname,
                Group = student.Group.Name
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student == null)
            {
                Response.StatusCode = 404;
                return Json(new
                {
                    message = "Tələbə tapılmadı"
                },JsonRequestBehavior.AllowGet);
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Details(int id)
        {
            Student student = _context.Students.Include("Group").FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Response.StatusCode = 404;
                return Json(new
                {
                    message = "Tələbə tapılmadı"
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                student.Id,
                student.Name,
                student.Surname,
                Group = new
                {
                    student.Group.Id,
                    student.Group.Name
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;

                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

                return Json(errorList, JsonRequestBehavior.AllowGet);
            }

            _context.Entry(student).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            student.Group = _context.Groups.Find(student.GroupId);

            return Json(new
            {
                student.Id,
                student.Name,
                student.Surname,
                Group = student.Group.Name
            }, JsonRequestBehavior.AllowGet);
        }
    }
}