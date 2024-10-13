using Amazon_Api.Dtos;
using AutoMapper;
using EcommerceCoffe_DAL.Models.BasketModel;
using EcommerceCoffe_DAL.Models.Product;
using EcommerceCoffe_DAL.Prsistence.Repositories;
using EcommerceCoffee_BLL.DTOs;
using EcommerceCoffee_BLL.Service.IBasketService;
using EcommerceCoffee_BLL.Service.IProductService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace EcommerceCoffee_PL.Controllers
{

  
    public class ProductController : Controller
    {
        private readonly IProductServ _productServ;
        private readonly IBasketServ _basketServ;
        private readonly IMapper _mapper;
        public ProductController(IProductServ productServ, IBasketServ basketServ, IMapper mapper) //ask unity(CLR) To create object when i need 
        {
            _productServ = productServ;
            _basketServ = basketServ;
            _mapper = mapper;
        }


        [HttpGet] //GET: /Product/Index
        public async Task<ActionResult> Index()
        {
            try
            {

                var products = await _productServ.GetAllProductsAsync();
                var productDtos = _mapper.Map<IEnumerable<ProductRetuenDto>>(products);
                if (products == null)
                    return HttpNotFound("No Products Found");

                return View(productDtos); //Product/Index
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while fetching products. Please try again later." + ex;
                return View("ErrorView"); //Product/ErrorView
            }
        }
     

        [HttpGet] //GET: /Product/Details/id
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var result = await _productServ.GetProductId(id);
                var productDtos = _mapper.Map<ProductRetuenDto>(result);

                if (productDtos == null)
                    return new HttpStatusCodeResult(400, "Bad Request");

                return View(productDtos);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while fetching youer product. Please try again later." + ex;
                return View("ErrorView"); //Product/ErrorView
            }

        }


        [HttpPost] // GET: /Product/AddToCart/id
        public async Task<ActionResult> AddToCart(int id)
        {
            try
            {
                
                var userId = Session["UserId"]?.ToString();

                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account"); 
                }

             
                var product = await _productServ.GetProductForCartByIdAsync(id);

                if (product == null)
                    return new HttpStatusCodeResult(400, "Bad Request");

              
                var basketItem = new BasketItem
                {
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl,
                    Price = product.Price,
                    Quantity = 1, // Default to 1
                    CategoryName = product.CategoryName.Name,
                    BrandName = product.ProductBrand.Name,
                    UserId = userId 
                };

               
                await _basketServ.AddToCartAsync(basketItem, userId); 

                return Json(new { success = true, message = "Item added to cart successfully" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching your product. Please try again later. " + ex.Message;
                return View("ErrorView"); // Product/ErrorView
            }
        }

        [HttpGet] //GET: /Product/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                var selecCategorytListItems = await _productServ.GetAllBrand();
                ViewData["BrandId"] = new SelectList(selecCategorytListItems, "id", "Name");

                var selecBrandtListItems = await _productServ.GetAllCategory();
                ViewData["CategoryId"] = new SelectList(selecBrandtListItems, "id", "Name");
                return View();
            }
            catch (Exception)
            {
                return View("ErrorView"); //Product/ErrorView
            }
        }

        [HttpPost] //POST: /Product/Create ==> From View 
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDto ProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ProductDto);

                if (ProductDto == null)
                    return new HttpStatusCodeResult(400, "Bad Request");
             
                else
                    ModelState.AddModelError(string.Empty, "Product is not Created");

                      await _productServ.CreateProducts(ProductDto);
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while Creating youer new Product. Please try again later." + ex;
                return View("ErrorView"); //Product/ErrorView
            }
        }


        [HttpGet] //GET: /Product/Update
        public async Task<ActionResult> Update(int id)
        {
            try
            {
                var result = await _productServ.GetProductId(id);
                var selecCategorytListItems = await _productServ.GetAllBrand();
                ViewData["BrandId"] = new SelectList(selecCategorytListItems, "id", "Name");

                var selecBrandtListItems = await _productServ.GetAllCategory();
                ViewData["CategoryId"] = new SelectList(selecBrandtListItems, "id", "Name");

                var resultView = await _productServ.GetProductDtoById(id);
                

                return View(resultView);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while Updating youer new Product. Please try again later." + ex;
                return View("ErrorView"); //Product/ErrorView
            }
           
        }

        [HttpPost] //POST: /Product/Update
        public async Task<ActionResult> Update(ProductDto productDto)
        {
            try
            {
                var result = await _productServ.UpdateProduct(productDto);

                if (!ModelState.IsValid)
                    return View(result);

                if (result != null)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Product is not Created");

                return View(result);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while Updating youer new Product. Please try again later." + ex;
                return View("ErrorView"); //Product/ErrorView
            }

        }

        [HttpPost] // or [HttpDelete] if you change the method type in ajax
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _productServ.DeleteProduct(id);
                return Json(new { success = true });  // return JSON to indicate success
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });  // return error message in case of failure
            }
        }
    }
}