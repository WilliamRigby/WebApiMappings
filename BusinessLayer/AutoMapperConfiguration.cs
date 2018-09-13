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

            CreateMap<EntityModels.Customer, BusinessLayer.Models.Customer>().ReverseMap();
            CreateMap<EntityModels.Order, BusinessLayer.Models.Order>().ReverseMap();
            CreateMap<EntityModels.OrderItem, BusinessLayer.Models.OrderItem>().ReverseMap();
            CreateMap<EntityModels.Product, BusinessLayer.Models.Product>().ReverseMap();
            CreateMap<EntityModels.Supplier, BusinessLayer.Models.Supplier>().ReverseMap();            
        }
    }
}
