using HtmlAgilityPack;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Threading.Tasks;
using WebMail.Classes;

namespace WebMail.Classes
{
    public class Mails
    {
        public InternetAddress Email { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string MailContent { get; set; }
    }

    public class Imap
    {
        public ImapClient Client { get; set; }
        public int MailCount { get; set; }
        public List<Mails> Mail { get; set; }
        

        public bool Login(string Email, string Password)
        {
            try
            {
                Client = new ImapClient();
                Client.Connect("imap.gmail.com", 993, true);
                Client.Authenticate(Email, Password);
                Client.Inbox.Open(FolderAccess.ReadOnly);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public List<Mails> ShowMail(string Email, string Password)
        {
            Login(Email, Password);
            Mail = new List<Mails>();
            var inbox = Client.Inbox;
            for (int i = 0; i < Client.Inbox.Count; i++)
            {
                Mails mail = new Mails();
                mail.Email = inbox.GetMessage(i).From[0];
                mail.Subject = inbox.GetMessage(i).Subject;
                mail.Date = inbox.GetMessage(i).Date.DateTime.ToShortDateString();
                Mail.Add(mail);
            }




            //MailCount = Client.Inbox.Count;
            //for (int i = MailCount; i >= 1; i--)
            //{
            //    MessageHeader headers = Client.GetMessageHeaders(i);
            //    var message = Client.GetMessage(i).MessagePart.MessageParts[0];
            //    var message1 = message.BodyEncoding.GetString(message.Body);
            //    Mails mail = new Mails();
            //    string from = Convert.ToString(headers.From);

            //    mail.Email = from.Split('<')[1].Split('>')[0];
            //    mail.Subject = headers.Subject;
            //    mail.Date = headers.Date;
            //    Mail.Add(mail);
            //}
            return Mail;
        }

        public string ShowMailContent(int count, string Email, string Password)
        {

            Login(Email, Password);
            var inbox = Client.Inbox.GetMessage(count);
            string message1 = inbox.GetTextBody(MimeKit.Text.TextFormat.Html);

            //List<char> chars = new List<char>();
            //bool endTag = false;
            //foreach (char item in message1)
            //{
            //    if (item == '>')
            //    {
            //        endTag = true;
            //    }
            //    if (item != '<' && endTag)
            //    {
            //        chars.Add(item);
            //        if (item == '<')
            //        {
            //            endTag = false;
            //        }
            //    }
            //}
            //foreach (char item in chars)
            //{
            //    message1 += item;
            //}
            return message1;
        }
    }

    public class DataBase
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Mail;Integrated Security=True";
        public DataBase()
        {

        }
        public void AddEmail(string email, string password)
        {
            if (!EmailExist(email))
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("addEmail", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmailInput", email);
                cmd.Parameters.AddWithValue("@PasswordInput", password);
                cmd.ExecuteNonQuery();
            }
        }
        public bool EmailExist(string email)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("ReadEmails", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (email == reader.GetString(0))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
