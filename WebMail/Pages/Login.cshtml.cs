using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            Connection Con = new Connection();
            if (Con.Login(Email, Password))
            {
                HttpContext.Session.SetString("Logged in", "1");
                return RedirectToPage("index");
            }
            else
            {
                return Page();
            }
        }
    }
}
