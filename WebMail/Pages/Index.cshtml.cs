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

namespace WebMail.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public int MailCount { get; set; }
        public List<Mails> Mail { get; set; }

        public void OnGet()
        {
            Pop3Client Client = new Pop3Client();
            Client.Connect("pop.gmail.com", 995, true);
            Client.Authenticate("testsmtp799@gmail.com", "aub89sas");

            Mail = new List<Mails>();

            MailCount = Client.GetMessageCount();
            for (int i = MailCount; i >= 1; i--)
            {
                MessageHeader headers = Client.GetMessageHeaders(i);
                string message = Convert.ToString(Client.GetMessage(i));
                Mails mail = new Mails();
                string from = Convert.ToString(headers.From);

                mail.Email = from.Split('<')[1].Split('>')[0];
                mail.Subject = headers.Subject;
                mail.Date = headers.Date;
                mail.MailContent = message;
                Mail.Add(mail);
            }
        }
    }
}
