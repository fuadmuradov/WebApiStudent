using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStudent.DTOs.GroupDTOs
{
    public class GroupGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupStudentsDto> Students { get; set; }
    }
}
