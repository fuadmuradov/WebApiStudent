using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStudent.DTOs.FileDTOs
{
    public class FilePostDto
    {
        public IFormFile files { get; set; }
    }
}
