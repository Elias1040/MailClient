using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
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
        public List<string> Head { get; set; }
        public int Count { get; set; }
        public IActionResult OnGet(int count)
        {
            if (HttpContext.Session.GetString("Logged in") != "1")
            {
                return RedirectToPage("Login");
            }
            Imap client = new Imap();
            Head = new List<string>();
            Head = client.ShowMailHead(count, HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"));
            Count = count;
            return Page();
        }

        public HtmlString GetHtml()
        {
            Imap client = new Imap();
            Content = client.ShowMailContent(Count, HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"));
            
            return new HtmlString(Content);
        }
    }
}
