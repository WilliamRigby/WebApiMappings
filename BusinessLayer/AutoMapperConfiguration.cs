using AutoMapper;

namespace BusinessLayer
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
            /* Entity -> BL Mappings */

            CreateMap<Entity.Models.Customer, BusinessLayer.Models.Customer>().ReverseMap();
            CreateMap<Entity.Models.Order, BusinessLayer.Models.Order>().ReverseMap();
            CreateMap<Entity.Models.OrderItem, BusinessLayer.Models.OrderItem>().ReverseMap();
            CreateMap<Entity.Models.Product, BusinessLayer.Models.Product>().ReverseMap();
            CreateMap<Entity.Models.Supplier, BusinessLayer.Models.Supplier>().ReverseMap();            
        }
    }
}
