﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStudent.DTOs.StudentDTOs
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int GroupId { get; set; }
        public string Groupname { get; set; }
    }
}
