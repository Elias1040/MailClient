using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebMail.Classes;

namespace WebMail.Pages
{
    public class MailModel : PageModel
    {
        public string Content { get; set; }
        public void OnGet(Mails mail)
        {
            Content = mail.MailContent;
         }
    }
}
