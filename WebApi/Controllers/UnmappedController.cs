using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.JsonConverters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnmappedController : ControllerBase
    {        
        private readonly IMapper _mapper;

        public UnmappedController()
        {
            _mapper = new AutoMapperConfiguration().Configure().CreateMapper();            
        }

        // GET api/Unmapped
        [HttpGet(Name = "GetUnmapped")]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {            
            CustomerService service = new CustomerService();

            IEnumerable<EntityModels.Customer> customers = await service.GetAllCustomersUnmapped();
            
            var jsonResolver = new CustomContractResolver();

            jsonResolver.IgnoreProperty(typeof(EntityModels.Order), "Customer");
            jsonResolver.IgnoreProperty(typeof(EntityModels.OrderItem), "Order");
            jsonResolver.IgnoreProperty(typeof(EntityModels.Product), "OrderItems");
            jsonResolver.IgnoreProperty(typeof(EntityModels.Supplier), "Products");

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = jsonResolver
            };

            var list = new List<string>();

            foreach(var c in customers)
            {
                list.Add(JsonConvert.SerializeObject(c, serializerSettings));
            }

            return list;
        }

        // GET api/Unmapped/5
        [HttpGet("{id}", Name = "GetUnmappedById")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/Unmapped
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Unmapped/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Unmapped/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
