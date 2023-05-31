using Sign_in_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sign_in_Application.Repository;


namespace Sign_in_Application.Controllers
{
    public class SigninController : Controller
    {
        
 
        /// <summary>
        /// View of Sign in page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }






        /// <summary>
        /// Compare username and password with database and give sign in functionality
        /// </summary>
        /// <param name="signin"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Signin(Signin signin, string returnUrl, bool keepSignedIn = false)
        {
            UserRepository userRepository = new UserRepository();
            var data = userRepository.SigninUser(signin.Username).Where(model => model.Username == signin.Username && model.Password == signin.Password).FirstOrDefault();
            if (data != null)
            {

                //Session["userid"] = signin.Id;
                Session["Username"] = signin.Username;
                if (keepSignedIn)
                {
                    
                        // Create a new authentication ticket with the username
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1,                             // Version
                            signin.Username,                      // Username
                            DateTime.Now,                  // Issue date
                            DateTime.Now.AddDays(7),       // Expiration date (e.g., 7 days from now)
                            true,                          // Is persistent
                            string.Empty                    // User data (optional)
                        );

                        // Encrypt the ticket
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                        // Create a new cookie and add the encrypted ticket
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                        // Set the cookie expiration
                        authCookie.Expires = authTicket.Expiration;

                        // Add the cookie to the response
                        Response.Cookies.Add(authCookie);
                    }
                //FormsAuthentication.SetAuthCookie(signin.Username, false);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    //FormsAuthentication.SetAuthCookie(signin.Username, false);
                    return Redirect(returnUrl);
                }
                else
                {
                   
                    return RedirectToAction("Dashboard");
                }
            

            }
            else
            {
                ViewBag.Showmsg = "Invalid Username or Password!!";
                ModelState.Clear();
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Signin");
            }

            ViewBag.Message = "Welcome!";
            return View();
        }

        /// <summary>
        /// Sign out the authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            //return View("Signin");

            // destroy the session state
            Session.Abandon();




            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();



            // redirect to the login page
            return RedirectToAction("Signin", "Signin");

        }
    }
}


















