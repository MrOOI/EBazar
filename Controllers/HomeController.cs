using EBazar.Data;
using EBazar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        [HttpGet]
        public IActionResult GetAllProduct(string maxPrice, string minPrice, string sortOrder, string searchString, int indexColumn, int? page)
        {

            ViewBag.maxPrice = maxPrice;
            ViewBag.minPrice = minPrice;
            ViewBag.searchString = searchString;
            ViewBag.page = page;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.IndexColumn = indexColumn;


            IndexViewModel indexModel = new IndexViewModel();

            IQueryable<Product> products = productContext.Products.
                         Where(s => (searchString == null || s.Name.Contains(searchString))
                            && (maxPrice == null || s.Cost <= int.Parse(maxPrice))
                            && (minPrice == null || s.Cost >= int.Parse(minPrice)));

            Pager pager = new Pager(products.Count(), page);

            switch (indexColumn)
            {
                case 0:
                    indexModel = sortOrder == "Date" ?
                    new IndexViewModel
                    {
                        Items = products.OrderBy(s => s.Name).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    } : new IndexViewModel
                    {
                        Items = products.OrderByDescending(s => s.Name).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    break;
                case 1:
                    indexModel = sortOrder == "Date" ?
                    new IndexViewModel
                    {
                        Items = products.OrderBy(s => s.Description).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    } : new IndexViewModel
                    {
                        Items = products.OrderByDescending(s => s.Description).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    break;
                case 2:
                    indexModel = sortOrder == "Date" ?
                    new IndexViewModel
                    {
                        Items = products.OrderBy(s => s.Cost).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    } : new IndexViewModel
                    {
                        Items = products.OrderByDescending(s => s.Cost).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    break;
                case 3:
                    indexModel = sortOrder == "Date" ?
                    new IndexViewModel
                    {
                        Items = products.OrderBy(s => s.AddTime).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    } : new IndexViewModel
                    {
                        Items = products.OrderByDescending(s => s.AddTime).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    break;
                case 4:
                    indexModel = sortOrder == "Date" ?
                    new IndexViewModel
                    {
                        Items = products.OrderBy(s => s.ExpireTime).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    } : new IndexViewModel
                    {
                        Items = products.OrderByDescending(s => s.ExpireTime).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    break;
                default:
                    indexModel = sortOrder == "Date" ?
                    new IndexViewModel
                    {
                        Items = products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    } : new IndexViewModel
                    {
                        Items = products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    break;
            }
            return View(indexModel);
        }

        [HttpGet]
        public IActionResult GetAllJson(string maxPrice, string minPrice, string sortOrder, string searchString, int indexColumn, int? page)
        {
          
            IndexViewModel indexModel = new IndexViewModel();

            IQueryable<Product> products = productContext.Products.
                         Where(s => (searchString == null || s.Name.Contains(searchString))
                            && (maxPrice == null || s.Cost <= int.Parse(maxPrice))
                            && (minPrice == null || s.Cost >= int.Parse(minPrice)));

            Pager pager = new Pager(products.Count(), page);

            switch (indexColumn)
            {
                case 0:
                    products = sortOrder == "ascending" ?
                    products.OrderBy(s =>s.Name) : products.OrderByDescending(s => s.Name);                                     
                    break;
                case 1:
                    products = sortOrder == "ascending" ?
                    products.OrderBy(s => s.Description) : products.OrderByDescending(s => s.Description);                    
                    break;
                case 2:
                    products = sortOrder == "ascending" ?
                    products.OrderBy(s => s.Cost) : products.OrderByDescending(s => s.Cost);                
                    break;
                case 3:
                    products = sortOrder == "ascending" ?
                    products.OrderBy(s => s.AddTime) : products.OrderByDescending(s => s.AddTime);                   
                    break;
                case 4:
                    products = sortOrder == "ascending" ?
                    products.OrderBy(s => s.ExpireTime) : products.OrderByDescending(s => s.ExpireTime);                  
                    break;
                default:                                     
                    break;
            }

            indexModel = new IndexViewModel
            {
                Items = products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            return Json(indexModel);
        }


        public ActionResult GetWithVue()
        {
            return View();
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

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
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
