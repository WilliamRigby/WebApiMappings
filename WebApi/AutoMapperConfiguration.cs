using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });

            return config;
        }
    }

    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() : this("AutoMapperProfiles") { }

        protected AutoMapperProfiles(string profileName) : base(profileName)
        {
            /* BL -> Api Mappings */

            CreateMap<BusinessLayer.Models.Customer, WebApi.Models.Customer>().ReverseMap();
            CreateMap<BusinessLayer.Models.Order, WebApi.Models.Order>().ReverseMap();
            CreateMap<BusinessLayer.Models.OrderItem, WebApi.Models.OrderItem>().ReverseMap();
            CreateMap<BusinessLayer.Models.Product, WebApi.Models.Product>().ReverseMap();
            CreateMap<BusinessLayer.Models.Supplier, WebApi.Models.Supplier>().ReverseMap();

        }
    }
}
