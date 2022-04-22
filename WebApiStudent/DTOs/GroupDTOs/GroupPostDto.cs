using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStudent.DTOs.GroupDTOs
{
    public class GroupPostDto
    {
        public string Name { get; set; }
    }

    public class GroupPostDtoValidation : AbstractValidator<GroupPostDto>
    {
        public GroupPostDtoValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Groups Name Must be Filled")
                .MaximumLength(40).WithMessage("Group Name character's maximum length 40");
        }
    }
}
