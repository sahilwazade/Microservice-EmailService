using EmailService.Application.IServices;
using EmailService.Infrastructure.IRepository;
using EmailService.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace EmailService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public UserService(IEmailService emailService, IUserRepository userRepository, IConfiguration config)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<GetUserByNameResponse> GetUserByName(GetUserByNameCommand command)
        {
            GetUserByNameResponse response = null;
            try
            {
                var result = await _userRepository.GetUserByName(command);
                if (result != null)
                {
                    var emailContent = await _userRepository.GetEmailTemplate(EmailTemplateTypes.SendMAilWhenGetsId);

                    var placeholders = emailContent.Placeholders.Split(',');
                    var placeholderValues = new Dictionary<string, string>();

                    foreach (var placeholder in placeholders)
                    {
                        var trimmedPh = placeholder.Trim();
                        if (trimmedPh == "{{EmpID}}")
                        {
                            placeholderValues[trimmedPh] = result.employee.EmpID.ToString();
                        }
                        if (trimmedPh == "{{EmpName}}")
                        {
                            placeholderValues[trimmedPh] = $"{command.FirstName} {command.LastName}";
                        }
                    }

                    foreach (var placeholder in placeholderValues)
                    {
                        emailContent.TemplateBody = emailContent.TemplateBody.Replace(placeholder.Key.ToString(), placeholder.Value);
                    }

                    var emailCommand = new EmailCommand()
                    {
                        To = result.employee.Email,
                        From = _config["From"],
                        Subject = emailContent.Subject,
                        Body = "",
                        IsHTMLBody = true,
                        HTMLView = emailContent.TemplateBody,
                        AttactmentFileName = "",
                        CcList = "",
                        AttachmentFileByteArray = []
                    };

                    var emailSend = _emailService.SendEmail(emailCommand);
                    if (!emailSend.IsSuccess)
                    {
                        return response = new GetUserByNameResponse
                        {
                            IsSuccess = false,
                            Message = $"Error of EmailService: { emailSend.Message }" + $"Error of UserRepository: { result.Message }"
                        };
                    }

                    return response = new GetUserByNameResponse
                    {
                        employee = result.employee,
                        IsSuccess = true,
                        Message = $"{result.Message} and Email Sent successfully to {command.FirstName} {command.LastName}({result.employee.Email})"
                    };
                }
            }
            catch (Exception ex)
            {
                return response = new GetUserByNameResponse
                {
                    IsSuccess = false,
                    Message = $"Error: { ex.Message }"
                };
            }
            return response;
        }

        //Add email related methods logic here ...
    }
}
