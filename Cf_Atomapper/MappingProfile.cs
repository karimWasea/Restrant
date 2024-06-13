using AutoMapper;

using Cf_Viewmodels;

using DataAcessLayers;

namespace Cf_Atomapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryVm>().ReverseMap();
            CreateMap<Product, productVM>().ReverseMap();
            CreateMap<PriceProductebytypes, PriceProductebytypesVM>().ReverseMap();

            //CreateMap<Data__Access__layer.Room, RoomlDto>().ForMember(dest => dest.DepartmentName,
            //    src => src.MapFrom(src => src.Department.DepartmentName)).ReverseMap();
        }
    }
}
