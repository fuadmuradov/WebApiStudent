using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStudent.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
