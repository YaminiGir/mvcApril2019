using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcApril2019.Models;

namespace mvcApril2019.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Details(int id)
        {
            EmployeeContext vEmployeeContext = new EmployeeContext();
            Employee employee = vEmployeeContext.Employees.Single(emp => emp.EmployeeId == id);

            return View(employee);
        }

        public ActionResult Index(int DepartmentID)
        {
            EmployeeContext mEmployeeContext = new EmployeeContext();
            List<Employee> mEmployee = mEmployeeContext.Employees.Where(emp => emp.DepartmentID == DepartmentID).ToList();
            return View(mEmployee);
        }

        // GET: Employee
        public ActionResult DetailsOld()
        {
            Employee employee = new Employee()
            {
                EmployeeId = 101,
                Name = "Suraj",
                Gender = "Male",
                City = "Mumbai"
            };

            return View(employee);
        }
    }
}