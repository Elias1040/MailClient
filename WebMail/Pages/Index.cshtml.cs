using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using System.Net;
using System.Net.Mail;
using WebMail.Classes;
using Microsoft.AspNetCore.Http;

namespace WebMail.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        
        public List<Mails> Mail { get; set; }
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("Logged in") != "1")
            {
                return RedirectToPage("Login");
            }

            PoP3 mails = new PoP3();
            Mail = mails.ShowMail(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"));
            
            return Page();
        }
    }
}
