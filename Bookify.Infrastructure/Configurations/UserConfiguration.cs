using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user =>  user.Id);
            builder.Property(user => user.FirstName)
                .HasMaxLength(200)
                .HasConversion(firstName => firstName.Value, value => new FirstName(value));

            builder.Property(user => user.LastName)
               .HasMaxLength(50)
               .HasConversion(lastName => lastName.Value, value => new LastName(value));

            builder.Property(user => user.Email)
               .HasMaxLength(50)
               .HasConversion(userEmail => userEmail.Value, value => new UserEmail(value));

            builder.Property(user => user.UserName)
               .HasMaxLength(50)
               .HasConversion(userName => userName.Value, value => new UserName(value));


        }
    }
}
