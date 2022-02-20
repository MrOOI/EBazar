using EBazar.Data;
using EBazar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EBazar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ProductContext productContext = new ProductContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult GetAllProduct(string maxPrice, string minPrice, string sortOrder, string searchString, int? page)
        {
            //PageInfo pageInfo = new PageInfo();
            //pageInfo.PageSize = 10;
            //pageInfo.TotalItems = productContext.Products.Count();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var product = from item in productContext.Products select item;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!string.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(maxPrice))
            {
                product = product.Where(s => s.Cost < int.Parse(maxPrice));
            }

            if (!String.IsNullOrEmpty(minPrice))
            {
                product = product.Where(s => s.Cost > int.Parse(minPrice));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    product = product.OrderBy(s => s.ExpireTime);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(s => s.AddTime);
                    break;
                default:
                   
                    break;
            }


            return View(product.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Add(Product product)
        {                    
            await productContext.Products.AddAsync(product);
            productContext.SaveChanges();
            return RedirectToAction("Create");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
