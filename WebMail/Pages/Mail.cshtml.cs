using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using OpenPop.Mime;
using WebMail.Classes;

namespace WebMail.Pages
{
    public class MailModel : PageModel
    {
        public string Content { get; set; }
        public void OnGet(int count)
        {
            PoP3 client = new PoP3();
            Content = client.ShowMailContent(count, HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"));
        }
    }
}
