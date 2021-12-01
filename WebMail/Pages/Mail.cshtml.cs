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
        public int Count { get; set; }
        public void OnGet(int count)
        {
            Imap client = new Imap();
            Content = client.ShowMailContent(count, HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"));
            Count = count;
            //return new HtmlString(Content);
        }
        public HtmlString GetHtml()
        {
            Imap client = new Imap();
            Content = client.ShowMailContent(Count, HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"));
            
            return new HtmlString(Content);
        }
    }
}
