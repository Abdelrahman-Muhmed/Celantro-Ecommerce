using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffee_BLL.Service.ICategorySevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCoffee.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServ _categoryServ;
        
        public CategoryController(ICategoryServ categoryServ)
        {
            _categoryServ = categoryServ;
        }

        [HttpGet]//GET: /Product/Index
        public async Task<ActionResult> Index()
        {
            try
            {
                var categories = await _categoryServ.GetAllCategory();
                if (categories == null)
                    return HttpNotFound("No categories Found");

                return View(categories); //Category/View
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching Category. Please try again later." + ex;
                return View("ErrorView"); //Category/ErrorView
            }
        }


        [HttpGet]//GET: /Product/Details/id
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var result = await _categoryServ.GetCategoryById(id);
                if (result == null)
                    return new HttpStatusCodeResult(400, "Bad Request");

                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching youer product. Please try again later." + ex;
                return View("ErrorView"); //Category/ErrorView
            }

        }

        [HttpGet]//GET: /Category/Create
        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]//POST: /Category/Create
        public async Task<ActionResult> Create(ProductCategory products)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(products);

                if (products == null)
                    return new HttpStatusCodeResult(400, "Bad Request");
                else
                    ModelState.AddModelError(string.Empty, "Product is not Created");

                await _categoryServ.CreateCatgories(products);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while Creating youer new Category. Please try again later." + ex;
                return View("ErrorView"); //Category/ErrorView
            }

        }


        [HttpGet]//GET: /Category/Update
        public async Task<ActionResult> Update(int id)
        {
            try
            {
                var resultView = await _categoryServ.GetCategoryById(id);
                return View(resultView);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while Updating youer new Category. Please try again later." + ex;
                return View("ErrorView"); //Product/ErrorView
            }

        }

        [HttpPost]//POST: /Category/Update
        public async Task<ActionResult> Update(ProductCategory products)
        {
            try
            {
                var result = await _categoryServ.UpdateCategory(products);

                if (!ModelState.IsValid)
                    return View(result);

                if (result != null)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Category is not Created");

                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while Updating youer new Category. Please try again later." + ex;
                return View("ErrorView"); //Category/ErrorView
            }
        }

 
        [HttpGet]//GET: /Category/Delete
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryServ.DeleteCategory(id);

                return Json(result);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while Deleting youer Category. Please try again later." + ex;
                return View("ErrorView"); //Category/ErrorView
            }

        }
    }
}