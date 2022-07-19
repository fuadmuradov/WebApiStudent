using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcStudent.Models
{
    public class FilePostDto
    {
        public IFormFile Files { get; set; }
    }
}
