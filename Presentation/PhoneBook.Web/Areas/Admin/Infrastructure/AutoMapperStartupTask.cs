using AutoMapper;
using PhoneBook.Admin.Models;
using PhoneBook.Core.Domain;
using PhoneBook.Core.Infrastructure;

namespace PhoneBook.Admin.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Department, DepartmentModel>();
                cfg.CreateMap<DepartmentModel, Department>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();
            });
        }

        public int Order => 0;
    }
}