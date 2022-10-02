using CountryProject.Models;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CountryProject.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _config;
        public EmailServices(IConfiguration config)
        {
            _config = config;
        }
        public bool SendMail(EmailModel model)
        {
            try
            {
                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient();
                m.From = new MailAddress(_config["SMTPConfig:From"]);
                m.To.Add(new MailAddress(model.Receiver));

                m.IsBodyHtml = true;
                m.Subject = model.Subject;
                m.Body = model.HtmlBody;
                sc.Host = _config["SMTPConfig:Host"];
                sc.Port = 25;
                sc.Credentials = new NetworkCredential(_config["SMTPConfig:UserName"], _config["SMTPConfig:Password"]);
                sc.EnableSsl = false;
                sc.Send(m);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            return true;
        }
    }
}
