using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiStudent.Data.Entities;

namespace WebApiStudent.Data.Configurations
{
    public class StudentConfiguration:IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(40);
            builder.Property(x => x.GroupId).IsRequired();
        }
    }
}
