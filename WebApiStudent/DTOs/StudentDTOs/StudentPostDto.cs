using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStudent.DTOs.StudentDTOs
{
    public class StudentPostDto
    {
        public string FullName { get; set; }
        public int GroupId { get; set; }
    }

    public class StudentPostDtoValidation : AbstractValidator<StudentPostDto>
    {
        public StudentPostDtoValidation()
        {
            RuleFor(x => x.FullName).NotNull().WithMessage("Fullname must be filled")
                .MaximumLength(35).WithMessage("Fullname Length Must be Less then 35");
            RuleFor(x => x.GroupId).NotNull().WithMessage("GroupId Must be Filled")
                .GreaterThanOrEqualTo(1).WithMessage("GroupId Must be Greater than 1");
        }
    }

}
