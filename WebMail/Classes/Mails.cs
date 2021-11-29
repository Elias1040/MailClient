using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

    public class Connection
    {
        public bool Login(string Email, string Password)
        {
            try
            {
                Pop3Client Client = new Pop3Client();
                Client.Connect("pop.gmail.com", 995, true);
                Client.Authenticate(Email, Password);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }

    public class DB
    {
        private readonly string connectionString;
        public DB(IConfiguration config)
        {
            connectionString = config.GetConnectionString("Default");
        }
        public void AddEmail()
        {
            if (!EmailExist())
            {
                SqlConnection
            }
            else
            {

            }
        }
        public bool EmailExist()
        {
            return true;
        }
        
    }
}
