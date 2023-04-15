using Application.ViewModels;
using Applications.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<LoginRequestViewModel, User>();
            CreateMap<RegisterRequestViewModel, User>();
            CreateMap<UpdateRequestViewModel, User>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<LoginResponseViewModel, User>().ReverseMap();
            CreateMap<CustomerUpdateRequest, Customer>().ReverseMap();

        }
    }
}
