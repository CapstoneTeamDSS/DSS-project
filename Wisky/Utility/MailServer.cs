using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Wisky.Utility
{
    public class MailServer
    {
        private static string MAIL_ADDRESS = "minhazuredemo@gmail.com";
        private static string PASSWORD = "zaQ@1234";


        public static bool SendEmailResetPassword(string email, string newPassword)
        {
            string subject = "[New Password]";
            //            string body = "Your new password is: "+ newPassword;
            string body = "We have reset your password. This is your new password: "
                          + " " + "<b>" + newPassword + "</b>";
            SendEmail(email, subject, body);
            return true;
        }

        private static bool SendEmail(string destinationEmail, string subject, string body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(MAIL_ADDRESS);
            msg.To.Add(destinationEmail);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(MAIL_ADDRESS, PASSWORD);
            client.Timeout = 20000;
            client.Send(msg);
            return true;
        }
    }
}
