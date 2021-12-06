using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WebMail.Classes;
using static WebMail.Classes.Imap;

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
            Imap Con = new Imap();
            if (Con.Login(Email, Password) != null)
            {
                //DataBase db = new DataBase();
                //db.AddEmail(Email, Password);
                try
                {
                    HttpContext.Session.SetString("Logged in", "1");
                    HttpContext.Session.SetString("Email", Email);
                    HttpContext.Session.SetString("Password", Password);
                    return RedirectToPage("index");
                }
                catch (Exception)
                {
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
