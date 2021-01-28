using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;


namespace QReduction.Apis.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendMail(string[] to, string subject, string body, string contentType = null, byte[] contentByte = null, string fileName =null)
        {
            var appSettings = _configuration.GetSection("AppSettings");

            string smtpServer = appSettings["mail_smtpServer"];
            int port = Convert.ToInt32(appSettings["mail_port"]);
            string userName = appSettings["mail_userName"];
            string password = appSettings["mail_password"];
            bool isSSl = true;//Convert.ToBoolean(appSettings["mail_isSSl"]);

            SmtpClient client = new SmtpClient(smtpServer, port);
            client.Credentials = new NetworkCredential(userName, password);
            client.EnableSsl = isSSl;
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(userName);
            mailMessage.BodyEncoding = Encoding.UTF8;

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            if (!string.IsNullOrEmpty(contentType))
            {
                var attach = new Attachment(contentStream: new MemoryStream(contentByte), contentType: new ContentType(contentType));
                attach.ContentDisposition.FileName = fileName;
                mailMessage.Attachments.Add(attach);
            }
            foreach (var item in to)
                mailMessage.To.Add(item);

            return client.SendMailAsync(mailMessage);
        }
    }
}
