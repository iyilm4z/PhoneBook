using System.Data.Entity.ModelConfiguration;
using PhoneBook.Core.Domain;

namespace PhoneBook.Data.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            ToTable("Department");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(100);
        }
    }
}