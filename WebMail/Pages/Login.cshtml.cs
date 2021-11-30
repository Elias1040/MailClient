using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OpenPop.Pop3;
using WebMail.Classes;

namespace WebMail.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            PoP3 Con = new PoP3();
            if (Con.Login(Email, Password))
            {
                DataBase db = new DataBase();
                db.AddEmail(Email, Password);
                HttpContext.Session.SetString("Logged in", "1");
                HttpContext.Session.SetString("Email", Email);
                HttpContext.Session.SetString("Password", Password);
                return RedirectToPage("index");
            }
            else
            {
                return Page();
            }
        }
    }
}
