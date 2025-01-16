using EmailService.Application.IServices;
using EmailService.Infrastructure.IRepository;
using EmailService.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                var emailCommand = new EmailCommand()
                {
                    To = response.employee.Email,
                    From = _config["MailSettings: From"],
                    Subject = "Add your subject.",
                    Body = "",
                    IsHTMLBody = true,
                    HTMLView = "",  //add your html template
                    AttactmentFileName = "",
                    CcList = "",
                    AttachmentFileByteArray = []
                };

                if (result != null)
                {
                    var emailSend = _emailService.SendEmail(emailCommand);
                    if (!emailSend.IsSuccess)
                    {
                        response = new GetUserByNameResponse
                        {
                            IsSuccess = false,
                            Message = $"Error of EmailService: { emailSend.Message }" + $"Error of UserRepository: { result.Message }"
                        };
                    }

                    response = new GetUserByNameResponse
                    {
                        employee = result.employee,
                        IsSuccess = true,
                        Message = $"{result.Message} and Email Sent successfully to {response.employee.Email}"
                    };
                }
            }
            catch (Exception ex)
            {
                response = new GetUserByNameResponse
                {
                    IsSuccess = false,
                    Message = $"Error: { ex.Message }"
                };
            }
            return response;
        }
    }
}
