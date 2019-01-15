using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.JsonConverters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappedController : ControllerBase
    {
        private readonly IMapper _mapper = new AutoMapperConfiguration().Configure().CreateMapper();            

        [HttpGet(Name = "GetMapped")]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            CustomerService service = new CustomerService();
            
            IEnumerable<Customer> customers = _mapper.Map<IEnumerable<Customer>>(await service.GetAllCustomersMapped());
            
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new CustomContractResolver 
                (
                    new Dictionary<Type, string> 
                    {
                        { typeof(Order), "Customer" },
                        { typeof(OrderItem), "Order" },
                        { typeof(Product), "OrderItems" },
                        { typeof(Supplier), "Products" }
                    }
                )
            };

            return customers.Select(c => JsonConvert.SerializeObject(c, settings)).ToList();
        }

        [HttpGet("{id}", Name = "GetMappedById")]
        public string Get(int id) { return "value"; }

        [HttpPost]
        public ActionResult<string> Post(object value) 
        { 
            return "{ \"result\" : \"success\"}";
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
