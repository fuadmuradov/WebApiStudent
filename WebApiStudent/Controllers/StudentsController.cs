using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiStudent.Data.DAL;
using WebApiStudent.Data.Entities;
using WebApiStudent.DTOs.StudentDTOs;

namespace WebApiStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext context;

        public StudentsController(StudentDbContext context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<StudentGetDto> studentGetDtos = context.Students.Include(x=>x.Group).Select(x => new StudentGetDto()
            {
                Id = x.Id,
                Fullname = x.Fullname,
                GroupId = x.GroupId, 
                Groupname = x.Group.Name
            }).ToList();

            return StatusCode(StatusCodes.Status200OK, studentGetDtos);

        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            Student student = context.Students.Include(x=>x.Group).FirstOrDefault(x=>x.Id==id);
            if (student == null) return NotFound();
            StudentGetDto studentGetDtos = new StudentGetDto()
            {
                Id = student.Id,
                Fullname = student.Fullname,
                GroupId = student.GroupId,
                Groupname = student.Group.Name
            };

            return StatusCode(StatusCodes.Status200OK, studentGetDtos);

        }


        [HttpPost("")]
        public async Task<IActionResult> Create(StudentPostDto studentPost)
        {
            Student student = new Student()
            {
                Fullname = studentPost.FullName,
                GroupId = studentPost.GroupId
            };

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, new {Id = student.Id, Fullname = student.Fullname, Group = student.Id });
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(int id, StudentPostDto studentPost)
        {
            Student student =await context.Students.FirstOrDefaultAsync(x=>x.Id==id);
            if (student == null) return NotFound();
            if (!context.Groups.Any(x => x.Id == studentPost.GroupId)) return BadRequest();
            student.Fullname = studentPost.FullName;
            student.GroupId = studentPost.GroupId;

            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            Student student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
            context.Students.Remove(student);
            context.SaveChanges();

            return Ok();
        }


    }
}
