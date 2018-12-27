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
        private readonly IMapper _mapper;

        public MappedController()
        {
            _mapper = new AutoMapperConfiguration().Configure().CreateMapper();            
        }

        // GET: api/Mapped
        [HttpGet(Name = "GetMapped")]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            CustomerService service = new CustomerService();
            
            IEnumerable<Customer> customers = _mapper.Map<IEnumerable<Customer>>(await service.GetAllCustomersMapped());
            
            CustomContractResolver resolver = new CustomContractResolver();

            resolver.IgnoreProperty(typeof(Order), "Customer");
            resolver.IgnoreProperty(typeof(OrderItem), "Order");
            resolver.IgnoreProperty(typeof(Product), "OrderItems");
            resolver.IgnoreProperty(typeof(Supplier), "Products");

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = resolver
            };

            List<string> list = new List<string>();

            foreach(var c in customers)
            {
                list.Add(JsonConvert.SerializeObject(c, settings));
            }

            return list;
        }

        // GET: api/Mapped/5
        [HttpGet("{id}", Name = "GetMappedById")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mapped
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Mapped/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
