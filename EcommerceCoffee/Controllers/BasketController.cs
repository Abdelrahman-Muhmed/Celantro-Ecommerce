using EcommerceCoffe_DAL.Models.BasketModel;
using EcommerceCoffe_DAL.Models.OrderModel;
using EcommerceCoffe_DAL.Prsistence.Repositories;
using EcommerceCoffee_BLL.Service.IBasketService;
using EcommerceCoffee_BLL.Service.IOrderService;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCoffee.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketServ _basketServ;
        private readonly IOrderServ _OrderSer;

        public BasketController(IBasketServ basketServ, IOrderServ orderServ )
        {
            _basketServ = basketServ;
            _OrderSer = orderServ;
        }


        [HttpGet] // GET: /Basket/Index
        public async Task<ActionResult> Index()
        {
            try
            {
                if (Session["UserId"] == null || Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var userIdSe = Session["UserId"].ToString();
                var basketItems = await _basketServ.GetAllBasketItemAsync(userIdSe);

                if (basketItems == null || !basketItems.Any())
                {
                    return new HttpStatusCodeResult(400, "Bad Request");
                }

                // Create order from basket items
                var order = new Order
                {
                    UserId = userIdSe,
                    OrderDate = DateTime.Now,
                    TotalAmount = basketItems.Sum(item => item.Price * item.Quantity),
                    OrderItems = basketItems.Select(item => new OrderItem
                    {
                        ProductName = item.ProductName,
                        PictureUrl = item.PictureUrl,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        CategoryName = item.CategoryName,
                        BrandName = item.BrandName
                    }).ToList()
                };

                // Return the new view for MyOrder instead of Index
                return View("myOrder", order);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching your basket. Please try again later. Error details: " + ex.Message;
                return View("ErrorView");
            }
        }


        [HttpPost] // POST: /Basket/CreateOrder
        public async Task<ActionResult> CreateOrder()
        {
            try
            {
               
                var userId = Session["UserId"]?.ToString();

                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account"); 
                }

                
                var basketItems = await _basketServ.GetAllBasketItemAsync(userId);

               


                await _OrderSer.CreateOrderAsync(userId, basketItems);

                return View("OrderConfirmation");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while creating your order. Please try again later. " + ex.Message;
                return View("ErrorView"); 
            }
        }



        //[HttpGet]//GET: /Basket/Update 
        //public ActionResult Update(int id)
        //{
        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = "An error occurred while Updating youer Basket. Please try again later." + ex;
        //        return View("ErrorView"); //Basket/ErrorView
        //    }
        //}

        //[HttpGet]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var basketItemDelete = await _basketServ.DeleteAsync(id);

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {

        //        ViewBag.ErrorMessage = "An error occurred while Deleting  Basket. Please try again later." + ex;
        //        return View("ErrorView"); //Basket/ErrorView
        //    }

        //}

    }
}