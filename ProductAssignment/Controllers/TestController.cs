using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductAssignment.Models;
namespace ProductAssignment.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        ProductDBEntities entity = new ProductDBEntities();

        public ActionResult Index(string name,string ASC,string DEC)
        {
            if(name!=null)
            {
                var Productname = (from t in entity.ProductTbls
                                   where t.ProductName == name
                                   select t).ToList();
                return View(Productname);
            }
            else if(ASC== "acending")
            {
                var Asc = (from t in entity.ProductTbls
                           orderby t.ProductName
                           select t).ToList();
                return View(Asc);
            }
            else if(DEC== "decending")
            {

                var Asc = (from t in entity.ProductTbls
                           orderby t.ProductName descending
                           select t).ToList();
                return View(Asc);
            }
            else
            {
                return View(entity.ProductTbls.ToList().OrderByDescending(p => p.ProductID));
            }
          
          
        }

        [HttpGet]
        public ActionResult Create()
        {
            colorDataDropdown(); // Method for Color Dropdown
            return View();
        }
        [HttpPost]
        public ActionResult Create(product rec)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductTbl ptbl = new ProductTbl();
                    colorDataDropdown();
                    var CheckName = entity.ProductTbls.Any(p => p.ProductName == rec.productName);
                    if (CheckName == true)
                    {
                        TempData["already"] = "Product Name is Already Exist in Database..";
                        return View();
                    }
                    ptbl.ProductName = rec.productName;
                    ptbl.Price = rec.Price;
                    ptbl.Quantity = rec.Quantity;
                    ptbl.IsGstApplicable = rec.IsGSTApplicable;
                    ptbl.PurchaseDate = Convert.ToDateTime(rec.Purchase_Date);
                    ptbl.ExpiryDate = Convert.ToDateTime(rec.Expiry_Date);
                    ptbl.Color = rec.ColorID;
                    entity.ProductTbls.Add(ptbl);
                    entity.SaveChanges();
                    TempData["save"] = "Record Inserted Successfully..";
                    return RedirectToAction("Index");

                }
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }


          public void colorDataDropdown()
          {
            List<product> pr = new List<product>();
            pr.Add(new product() { ColorID = 1, ColorName = "Red" });

            pr.Add(new product() { ColorID = 2, ColorName = "Black" });

            pr.Add(new product() { ColorID = 3, ColorName = "Pink" });

            pr.Add(new product() { ColorID = 4, ColorName = "White" });

            pr.Add(new product() { ColorID = 5, ColorName = "Purple" });

            ViewBag.color = new SelectList(pr.ToList(), "ColorID", "ColorName");
        }
        
       
        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            ProductTbl data = entity.ProductTbls.Find(id);
            product p = new product();
            p.productName = data.ProductName;
            p.ProductID = data.ProductID;
            p.Price = data.Price;
            p.IsGSTApplicable = data.IsGstApplicable;
            p.Quantity = data.Quantity;
            p.Purchase_Date =  data.PurchaseDate.Value.ToShortDateString();
            p.Expiry_Date = data.ExpiryDate.Value.ToShortDateString();

            List<product> pr = new List<product>();
            pr.Add(new product() { ColorID = 1, ColorName = "Red" });
          
            pr.Add(new product() { ColorID = 2, ColorName = "Black" });

            pr.Add(new product() { ColorID = 3, ColorName = "Pink" });

            pr.Add(new product() { ColorID = 4, ColorName = "White" });

            pr.Add(new product() { ColorID = 5, ColorName = "Purple" });
            pr.Add(new product() { ColorID = 6, ColorName = "Black" });
            ViewBag.color = new SelectList(pr.ToList(), "ColorID", "ColorName",data.Color);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(product rec)
        {
            ProductTbl data = entity.ProductTbls.Find(rec.ProductID);
            data.ProductID = rec.ProductID;
            data.ProductName = rec.productName;
            data.Price = rec.Price;
            data.Quantity = rec.Quantity;
            data.IsGstApplicable = rec.IsGSTApplicable;
            data.PurchaseDate = Convert.ToDateTime(rec.Purchase_Date);
            data.ExpiryDate = Convert.ToDateTime(rec.Expiry_Date);
            entity.Entry(data).State = System.Data.Entity.EntityState.Modified;
            entity.SaveChanges();
            TempData["edit"] = "Record Updated Successfully..";
            return RedirectToAction("Index");
           
        }
        public ActionResult Details(Int64 id)
        {
            ProductTbl tbl = entity.ProductTbls.Find(id);

            return View(tbl);
        }
        public ActionResult Delete(Int64 id)
        {
            ProductTbl tbl = entity.ProductTbls.Find(id);
            entity.ProductTbls.Remove(tbl);
            entity.SaveChanges();
            TempData["delete"] = "Record Deleted Successfully..!";
            return RedirectToAction("Index");
        }
    }
    }
