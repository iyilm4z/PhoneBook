using System.Data.Entity.ModelConfiguration;
using PhoneBook.Core.Domain;

namespace PhoneBook.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(u => u.Id);
            Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            Property(u => u.LastName).IsRequired().HasMaxLength(100);
            Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15);

            HasOptional(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId);
        }
    }
}