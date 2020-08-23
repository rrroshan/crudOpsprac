using crudOpsPrac.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudOpsPrac.Controllers
{
    public class EmployeeController : Controller
    {

        db_empdataEntities dbObj = new db_empdataEntities();
        // GET: Employee
        public ActionResult Employee(emp_table obj)
        {
            
            return View(obj);
            

                
        }

        [HttpPost]
        public ActionResult AddEmployee(emp_table model)      //emptable will bring the data from the form
        {
            emp_table obj = new emp_table();                    // add that data to table obj
            if (ModelState.IsValid) {

               
                obj.ID =  model.ID;
                obj.empName = model.empName;
                obj.empFname = model.empFname;
                obj.Email = model.Email;
                obj.Mobile_Phone = model.Mobile_Phone;
                obj.Description = model.Description;

                if (model.ID == 0)
                {
                    dbObj.emp_table.Add(obj);                                 // add the whole data to the database 
                    dbObj.SaveChanges();


                }
                else {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();

                }
            }
            ModelState.Clear();
            return View("Employee");
        }

        public ActionResult EmployeeList() {

            var res = dbObj.emp_table.ToList();


            return View(res);

        }

        public ActionResult Delete(int id) 
        {
            var res = dbObj.emp_table.Where(x => x.ID == id).First();

            dbObj.emp_table.Remove(res);
            dbObj.SaveChanges();
           
            var list = dbObj.emp_table.ToList();


            return View("EmployeeList", list);

        }
    }
}