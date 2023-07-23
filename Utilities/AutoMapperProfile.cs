using AutoMapper;
using BackEndAPI.DTO;
using BackEndAPI.Models;
using System.Globalization;

namespace BackEndAPI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            #endregion

            #region
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(
                dest => dest.DepartmentName,
                opt => opt.MapFrom(origin => origin.IdDeparmentNavigation.FullName)
                )
                .ForMember(
                dest => dest.HireDate,
                opt => opt.MapFrom(origin => origin.HireDate.Value.ToString("dd/MM/yyyy")
                ))
                .ForMember(dest => dest.Salary,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Salary, CultureInfo.InvariantCulture)));


            CreateMap<EmployeeDTO, Employee>()
                .ForMember(
                dest => dest.IdDeparmentNavigation,
                opt => opt.Ignore()
                )
                .ForMember(
                dest => dest.HireDate,
                opt => opt.MapFrom(origin =>
                DateTime.ParseExact(origin.HireDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                )
                .ForMember(
                dest => dest.Salary,
                opt => opt.MapFrom(origin =>
                Decimal.Parse(origin.Salary, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture))
                );
            #endregion
        }
    }
}
