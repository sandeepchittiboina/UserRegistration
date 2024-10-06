using Accounts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Accounts.Controllers
{ 
    public class AccountController : Controller
    {
        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the user registration data
                // For example, save the data into a database

                ViewBag.Message = "Registration successful!";
                return RedirectToAction("Success");
            }

            // If model state is invalid, return to the form
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
