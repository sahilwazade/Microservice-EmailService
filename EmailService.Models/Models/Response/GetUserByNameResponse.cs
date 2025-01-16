using EmailService.Models.Models;

namespace EmailService.Infrastructure.Repository
{
    public class GetUserByNameResponse : GenericResponse
    {
        public Employee employee { get; set; }
    }
}