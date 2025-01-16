using EmailService.Application.Services;
using EmailService.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.IServices
{
    public interface IEmailService
    {
        GenericResponse SendEmail(EmailCommand command);
    }
}
