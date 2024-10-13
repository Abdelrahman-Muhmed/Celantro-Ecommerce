using EcommerceCoffe_DAL.Model.IdentityModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using commerceCoffee_BLL.DTOs;
using EcommerceCoffee_BLL.DTOs;
using EcommerceCoffee_BLL.Service.IAuthServ;
using EcommerceCoffee_BLL.Helpers.Encryption;
using Microsoft.AspNet.Identity.EntityFramework;
using EcommerceCoffe_DAL.Prsistence.IdentityData;
using EcommerceCoffee_BLL.Service.IAccountService;
using EcommerceCoffe_DAL.Models.Product;
using System.Net;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
namespace EcommerceCoffee.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNet.Identity.UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser, string> _signInManager;
        private readonly IAuthServ _authServ;
        private readonly IAccountServ _accountServ;
        private readonly IMapper _mapper;


        public AccountController(
            Microsoft.AspNet.Identity.UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser, string> signInManager,
            IAuthServ authServ,
            IAccountServ accountServ,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authServ = authServ;
            _accountServ = accountServ;
            _mapper = mapper;


        }


        [HttpGet]
        public ActionResult Login()
        {
            
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> Login(loginDto loginDto)
        {
            // Decrypt the encrypted password from the request
            var secretKey = "b14ca5898a4e4133bbce2ea2315a1916";
            string decryptedPassword;

            decryptedPassword = EncryptionHelper.DecryptString(secretKey,loginDto.PassWord);

            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(400, "Invalid Login");
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return new HttpStatusCodeResult(401, "Unauthorized");
            }


            //if (user.IsLoggedIn)
            //{
               
            //    return new HttpStatusCodeResult(403, "User already logged in from another device.");

            //}

            var result = await _signInManager.PasswordSignInAsync(user.UserName, decryptedPassword, isPersistent: false, shouldLockout: false);

            if (result != SignInStatus.Success)
            {
                return new HttpStatusCodeResult(401, "Unauthorized");

            }

            // Mark user as logged in
            user.IsLoggedIn = true;
            user.LastLoginTime = DateTime.UtcNow;

            // Generate a unique Session ID
            string sessionId = Guid.NewGuid().ToString();
            user.CurrentSessionId = sessionId;

            await _userManager.UpdateAsync(user);

            Session["UserId"] = user.Id;
            Session["UserName"] = user.UserName;
            Session["Email"] = user.Email;
            Session["SessionId"] = sessionId;

            var token = await _authServ.CreateTokenAsync(user, _userManager);
            Session["AuthToken"] = token;

            // Sign in the user with additional parameter for rememberBrowser
            await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            var userDto = new UserDto
            {
                Email = user.Email,
                Name = user.UserName,
                //password = decryptedPassword,
                Token = token
            };


            return Json(userDto);

            // Return error response
            //return Json(new { success = false, message = "Invalid email or password." });

        }
        public ActionResult Logout()
        {
            var userId = Session["UserId"] as string;
            if (!string.IsNullOrEmpty(userId))
            {
               
                var user = _userManager.FindById(userId);

                if (user != null)
                {
                    user.IsLoggedIn = false;
                    user.CurrentSessionId = null;
                    _userManager.Update(user);
                }
            }

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Register() 
        {
            return View();
        
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(400, "Invalid registration details.");
            }

            var user = new ApplicationUser
            {
                Email = register.Email,
                Name = register.Name,
                UserName = register.Email.Split('@')[0],
                PhoneNumber = register.phoneNumber

            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                // Return error messages
                return new HttpStatusCodeResult(400, string.Join(", ", result.Errors));
            }

            // Generate token for the user
            var token = await _authServ.CreateTokenAsync(user, _userManager);

            // Return user data
            var userDto = new UserDto
            {
                Name = register.Name,
                Email = register.Email,
                Token = token
            };

            //return Json(userDto);

            return View("EmailConfirm");

        }
        [HttpPost] // POST: /Account/UpdateUser
        public async Task<ActionResult> UpdateUser(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(400, "Invalid update details.");
            }

            // Use the ID directly from the form submission
            var userId = register.Id;

            // You can skip finding the user from the database since you're directly using the ID from the form
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return HttpNotFound("User not found.");
            }

            // Update user properties
            user.Name = register.Name;
            user.Email = register.Email;
            user.PhoneNumber = register.phoneNumber;

          

            // Save changes (if applicable)
            await _userManager.UpdateAsync(user);

            // Redirect or return a view as needed
            return RedirectToAction("Index");
        }




        [HttpGet] //GET: /Account/Index
        public async Task<ActionResult>Index()
        {
            try
            {
                var useres = await _accountServ.GetAllUsers();
                var useresDto = _mapper.Map<IEnumerable<ApplicationUserReturnDto>>(useres);
                if (useresDto == null)
                    return HttpNotFound("No Products Found");

                return View(useresDto);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while fetching Useres. Please try again later." + ex;
                return View("ErrorView"); //Account/ErrorView
            }

        }


      


        [HttpGet]//GET: /Account/Create
        public ActionResult Create()
        {
           
            return View(); // Rerturn to Register
        }



        [HttpGet] // GET: /Account/Update
        public async Task<ActionResult> Update(string id)
        {
            // Retrieve the user by ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound("User not found.");
            }

           
            var updateUserDto = new RegisterDto
            {
                Name = user.Name,
                Email = user.Email,
                phoneNumber = user.PhoneNumber
                // Add any other properties as needed
            };

            return View(updateUserDto); // Pass the user data to the view
        }




        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _accountServ.DeleteUser(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("ErrorView");
        }

    }
}