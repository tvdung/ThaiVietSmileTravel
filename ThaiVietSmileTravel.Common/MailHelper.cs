using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;

using ThaiVietSmileTravel.Models.Framework;

namespace ThaiVietSmileTravel.Common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string name, string content, int contact, int custom)
        {
            var admin = new TVSTravelDbContext().tbl_Administrator.Where(x => x.IsAdmin);
            string fromEmailDisplayName;
            var tblAdministrator = admin.FirstOrDefault();
            if (tblAdministrator != null)
            {
                var fromEmailAddress = tblAdministrator.Email; // ConfigurationManager.AppSettings["FromEmailAddress"].ToString();

                //var toEmailAddress = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                var fromEmailPassword = tblAdministrator.PasswordEmail; //ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                if (contact == 1)
                {
                    if (custom == 0)
                    {
                        fromEmailDisplayName = Globalization.Resource.lblFromEmailDisplayNameContact + " " + name;
                    }
                    else
                    {
                        fromEmailDisplayName = Globalization.Resource.lblConfigFromEmailDisplayNameContact;
                    }
                }
                else
                {
                    if (custom == 0)
                    {
                        fromEmailDisplayName = Globalization.Resource.lblFromEmailDisplayNameOder + " " + name;
                    }
                    else
                    {
                        fromEmailDisplayName = Globalization.Resource.lblConfigFromEmailDisplayNameOder;
                    }
                }

                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
                bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

                string body = content;
                MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient();
                client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = enabledSsl;
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                client.Send(message);
            }
        }
    }
}