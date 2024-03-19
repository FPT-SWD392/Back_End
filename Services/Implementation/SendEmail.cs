using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class SendEmail : ISendEmail
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "lehoangson2259@gmail.com";
            var pw = "ktul fsea uhwq lzet";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)

            };
            return client.SendMailAsync(new MailMessage(from: mail,
                                                        to: email,
                                                        subject,
                                                        message));
        }
    }
}
