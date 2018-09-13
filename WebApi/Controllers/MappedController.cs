using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
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

            IEnumerable<BusinessLayer.Models.Customer> unmapped = await service.GetAllCustomersMapped();
            
            IEnumerable<Customer> customers = _mapper.Map<IEnumerable<Customer>>(unmapped);

            var jsonResolver = new CustomContractResolver();

            jsonResolver.IgnoreProperty(typeof(Order), "Customer");
            jsonResolver.IgnoreProperty(typeof(OrderItem), "Order");
            jsonResolver.IgnoreProperty(typeof(Product), "OrderItems");
            jsonResolver.IgnoreProperty(typeof(Supplier), "Products");

            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = jsonResolver
            };

            var list = new List<string>();

            foreach(var c in customers)
            {
                list.Add(JsonConvert.SerializeObject(c, serializerSettings));
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
