using EmailService.Application.IServices;
using EmailService.Models.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net.Mime;

namespace EmailService.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly MailConfiguration _mailConfiguration;
        public EmailService(IConfiguration config, MailConfiguration mailConfiguration)
        {
            _config = config;
            _mailConfiguration = mailConfiguration;
        }

        public GenericResponse SendEmail(EmailCommand command)
        {
            MailMessage message = new MailMessage();
            var recipients = command.To.Split('|');
            foreach (var r in recipients)
            {
                message.To.Add(new MailAddress(r));
            }

            if (!string.IsNullOrEmpty(command.CcList))
            {
                foreach (var r in command.CcList.Split('|'))
                {
                    if (!string.IsNullOrEmpty(r))
                    {
                        message.CC.Add(new MailAddress(r));
                    }
                }
            }

            message.Subject = command.Subject;
            message.Body = command.Body;
            message.From = new MailAddress(command.From);

            if (command.AttachmentFileByteArray?.Length > 0)
            {
                Attachment att = new Attachment(new MemoryStream(command.AttachmentFileByteArray), command.AttactmentFileName);
                message.Attachments.Add(att);
            }
            else if (command.AttactmentFileName != "")
            {
                //code uses attachmentFIlename as a full path not just the actual file name
                Attachment atchmnt = new Attachment(command.AttactmentFileName, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = atchmnt.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(command.AttactmentFileName);
                disposition.ModificationDate = File.GetLastWriteTime(command.AttactmentFileName);
                disposition.ReadDate = File.GetLastAccessTime(command.AttactmentFileName);
                disposition.FileName = Path.GetFileName(command.AttactmentFileName);
                disposition.Size = new FileInfo(command.AttactmentFileName).Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                message.Attachments.Add(atchmnt);
            }

            if (command.IsHTMLBody)
            {
                // Add the alternate body to the message.
                ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(command.HTMLView, mimeType);
                message.AlternateViews.Add(alternate);
            }

            SmtpClient smtpClient = new SmtpClient(_mailConfiguration.Host, Convert.ToInt32(_mailConfiguration.Port));
            smtpClient.Credentials = new System.Net.NetworkCredential(_mailConfiguration.UserName, _mailConfiguration.Password) ;         
            smtpClient.EnableSsl = Convert.ToBoolean(_mailConfiguration.EnableSsl);

            try
            {
                smtpClient.Send(message);
                return new GenericResponse()
                {
                    IsSuccess = true,
                    Message = $"Email sent successfully to { command.To }"
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse()
                {
                    IsSuccess = true,
                    Message = $"Email not sent, Error occured: { ex.Message }"
                };
            } 
        }
    }
}
