using mvcApril2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace mvcApril2019.Controllers
{
    public class NewHomeController : Controller
    {
        // GET: NewHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Product> employeeList = db.Products.ToList<Product>();
                return Json(new { data = employeeList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase xmlFile)
        {
            if (xmlFile.ContentType.Equals("application/xml") || xmlFile.ContentType.Equals("text/xml"))
            {
                var xmlPath = Server.MapPath("~/FileUpload" + xmlFile.FileName);
                xmlFile.SaveAs(xmlPath);
                XDocument xDoc = XDocument.Load(xmlPath);
                List<Product> productList = xDoc.Descendants("product").Select
                    (product => new Product
                    {
                        Id = Convert.ToInt32(product.Element("id").Value),
                        Name = product.Element("name").Value,
                        Price = Convert.ToDecimal(product.Element("price").Value),
                        Quantity = Convert.ToInt32(product.Element("quantity").Value)
                    }).ToList();

                using (DBModel db = new DBModel())
                {
                    foreach (var i in productList)
                    {
                        var v = db.Products.Where(a => a.Id.Equals(i.Id)).FirstOrDefault();

                        if (v != null)
                        {
                            v.Id = i.Id;
                            v.Name = i.Name;
                            v.Price = i.Price;
                            v.Quantity = i.Quantity;
                        }
                        else
                        {
                            db.Products.Add(i);
                        }
                        db.SaveChanges();
                    }
                }
                ViewBag.Success = "File uploaded successfully..";
            }
            else
            {
                ViewBag.Error = "Invalid file(Upload xml file only)";
            }
            return View("Index");
        }
    }
}