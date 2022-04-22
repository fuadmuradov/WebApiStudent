using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using WebApiStudent.Data.DAL;
using WebApiStudent.Data.Entities;
using WebApiStudent.DTOs.GroupDTOs;

namespace WebApiStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly StudentDbContext context;

        public GroupsController(StudentDbContext context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            //List<Group> groups = context.Groups.ToList();
            List<GroupGetDto> groupGets = context.Groups.Select(x => new GroupGetDto()
            {
                Id = x.Id,
                Name = x.Name,
                Students = x.Students.Select(y=> new GroupStudentsDto() { 
                Fullname = y.Fullname
                }).ToList()
            }).ToList();


            return StatusCode(StatusCodes.Status200OK, groupGets);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id=1)
        {
              Group group = await context.Groups.FirstOrDefaultAsync(x => x.Id == id);

            GroupGetDto groupGet = new GroupGetDto()
            {
                Id = group.Id,
                Name = group.Name,
                Students = group.Students.Select(y => new GroupStudentsDto() { }).ToList()
            };




            return Ok(groupGet);

        }

        [HttpPost("")]
        public async Task<IActionResult> Create(GroupPostDto groupPost)
        {
            Group group = new Group()
            {
                Name = groupPost.Name
            };

            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new {Id=group.Id, Name = group.Name });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Group group = await context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            context.Groups.Remove(group);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
