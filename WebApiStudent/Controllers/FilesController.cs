using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiStudent.Data.DAL;
using WebApiStudent.Data.Entities;
using WebApiStudent.DTOs.FileDTOs;
using WebApiStudent.Extensions;

namespace WebApiStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly StudentDbContext context;
        private readonly IWebHostEnvironment webHost;

        public FilesController(StudentDbContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            this.webHost = webHost;
        }

        [HttpGet]
        public async Task<IActionResult> GetFile()
        {

            List<Sfile> sfiles = context.Sfiles.ToList();
            return StatusCode(404, sfiles);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile([FromForm] FilePostDto postDto)
        {

            if(postDto.files != null)
            {
                Sfile sfile = new Sfile();
                sfile.Name = postDto.files.FileName;

                string folder = @"files\";
               sfile.FilePath = await postDto.files.SavaAsync(webHost.WebRootPath, folder);
               await context.Sfiles.AddAsync(sfile);
               await context.SaveChangesAsync();
                return StatusCode(200);
            }

            return StatusCode(404);
        }

    }
}
