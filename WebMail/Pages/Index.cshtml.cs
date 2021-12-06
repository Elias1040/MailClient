using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenPop.Mime.Header;
using System.Net;
using System.Net.Mail;
using WebMail.Classes;
using Microsoft.AspNetCore.Http;
using MimeKit;

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

            Imap mails = new Imap();
            Mail = mails.ShowMail(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"));
            
            return Page();
        }

        [BindProperty]
        public string Receiver { get; set; }
        [BindProperty]
        public string Subject { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("Logged in") != "1")
            {
                return RedirectToPage("Login");
            }
            string Sender = HttpContext.Session.GetString("Email");
            string password = HttpContext.Session.GetString("Password");
            Imap mail = new Imap();
            mail.SendMessage(Sender, Receiver, Subject, Message, password);

            return RedirectToPage("index");
        }
    }
}
