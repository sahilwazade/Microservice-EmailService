using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Models.Models
{
    public class GenericResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; } = string.Empty;
    }
}
