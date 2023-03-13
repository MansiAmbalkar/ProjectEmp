using ProjectEmp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectEmp.Controllers
{
    public class HomeController : Controller

    {
        EmpDetailDBContext _Context = new EmpDetailDBContext();
       

        public ActionResult Index(string sortOrder)
        {
            var EmpDetailsList = _Context.EmpDetails.ToList();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder)?"name_desc" : "";
            var EmpDetails = from x in _Context.EmpDetails select x;
            switch (sortOrder)
            {
                case "name_desc":
                    EmpDetails = EmpDetails.OrderByDescending(x => x.Name);
                    break;
                default:
                    EmpDetails = EmpDetails.OrderBy(x => x.Name);
                    break;

            }
            return View(EmpDetailsList);
        }
        [HttpGet]

        public ActionResult Create()

        {
            return View();



        }
        [HttpPost]
        public ActionResult Create(EmpDetail model)
        {
            var emp = _Context.EmpDetails.Where(x => x.EmpId == model.EmpId).FirstOrDefault();
            /*
            if (emp == null)
            {


                _Context.EmpDetails.Add(model);

                _Context.SaveChanges();

                return View();
            }
            else
            {
                return Content("<h4> No repetation of ID");
            }
            */
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var emp = _Context.EmpDetails.Where(x => x.EmpId == Id).FirstOrDefault();
            
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(EmpDetail model)
        {
            var emp = _Context.EmpDetails.Where(x => x.EmpId == model.EmpId).FirstOrDefault();
            if (emp != null)
            {
                emp.Name = model.Name;
                emp.Salary = model.Salary;
                emp.EmpId = model.EmpId;
                emp.Department = model.Department;
                emp.Degisnation = model.Degisnation;
                emp.ManagerId = model.ManagerId;
                _Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        
        public ActionResult Delete(int Id)
        {
            var emp = _Context.EmpDetails.Where(x=>x.EmpId == Id).FirstOrDefault();
            return View(emp);
        }
        [HttpPost]
        public ActionResult Delete(EmpDetail objEmpDetail) 
        {
          var emp = _Context.EmpDetails.Where(x => x.EmpId == objEmpDetail.EmpId).FirstOrDefault();
        _Context.EmpDetails.Remove(emp);
        _Context.SaveChanges();
        return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id) 
        {
            var emp = _Context.EmpDetails.Where(x=>x.EmpId == id ).FirstOrDefault();
            return View(emp);
        }
    }
}