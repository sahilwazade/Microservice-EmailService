using EmailService.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        Task<GetUserByNameResponse> GetUserByName(GetUserByNameCommand command);
    }
}
