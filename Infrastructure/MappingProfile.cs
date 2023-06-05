using AutoMapper;
using ProductManagement.Entities;
using ProductManagement.Model;
using ProductManagement.Requests;
using ProductManagement.Requests.BrandRequest;
using ProductManagement.Requests.WareHouseRequest;

namespace ProductManagement.Infrastructure
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BrandModel, Brand>();
            CreateMap<Brand, BrandModel>();
            CreateMap<ProductAddRequest, Product>();
            CreateMap<BrandAddRequet, Brand>();
            CreateMap<WareHouseAddRequest, WareHouse>();
        }
    }
}
