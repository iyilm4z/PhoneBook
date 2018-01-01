#region Usings

using AutoMapper;
using PhoneBook.Admin.Models;
using PhoneBook.Core.Domain;

#endregion

namespace PhoneBook.Admin.Infrastructure
{
    public static class MappingExtensions
    {
        #region Utils

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        #endregion

        #region Department

        public static DepartmentModel ToModel(this Department entity)
        {
            return entity.MapTo<Department, DepartmentModel>();
        }

        public static Department ToEntity(this DepartmentModel model)
        {
            return model.MapTo<DepartmentModel, Department>();
        }

        public static Department ToEntity(this DepartmentModel model, Department destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region User

        public static UserModel ToModel(this User entity)
        {
            return entity.MapTo<User, UserModel>();
        }

        public static User ToEntity(this UserModel model)
        {
            return model.MapTo<UserModel, User>();
        }

        public static User ToEntity(this UserModel model, User destination)
        {
            return model.MapTo(destination);
        }

        #endregion
    }
}