using EmailService.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.IServices
{
    public interface IUserService
    {
        Task<GetUserByNameResponse> GetUserByName(GetUserByNameCommand command);
    }
}
