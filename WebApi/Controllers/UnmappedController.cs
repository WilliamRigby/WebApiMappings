using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.JsonConverters;
using System.Linq;
using System;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnmappedController : ControllerBase
    {        
        private readonly IMapper _mapper = new AutoMapperConfiguration().Configure().CreateMapper();

        [HttpGet(Name = "GetUnmapped")]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {            
            CustomerService service = new CustomerService();

            IEnumerable<Entity.Models.Customer> customers = await service.GetAllCustomersUnmapped();
                        
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new CustomContractResolver 
                (
                    new Dictionary<Type, string> 
                    {
                        { typeof(Entity.Models.Order), "Customer" },
                        { typeof(Entity.Models.OrderItem), "Order" },
                        { typeof(Entity.Models.Product), "OrderItems" },
                        { typeof(Entity.Models.Supplier), "Products" }
                    }
                )
            };

            return customers.Select(c => JsonConvert.SerializeObject(c, settings)).ToList();
        }

        [HttpGet("{id}", Name = "GetUnmappedById")]
        public ActionResult<string> Get(int id) { return "value"; }

        [HttpPost]
        public void Post([FromBody] string value) { }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
