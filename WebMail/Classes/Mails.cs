using Microsoft.AspNetCore.Http;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMail.Classes
{
    public class Mails
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string MailContent { get; set; }
    }

    public class Conection
    {
        public void Login(string Email, string Password)
        {
            try
            {
                Pop3Client Client = new Pop3Client();
                Client.Connect("pop.gmail.com", 995, true);
                Client.Authenticate(Email, Password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CheckSession()
        {
            if (HttpContext.Session.GetString("Logged in") == "1")
            {

            }
        }
    }
}
