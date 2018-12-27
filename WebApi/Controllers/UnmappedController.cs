using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
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

            IEnumerable<Entity.Models.Customer> customers = await service.GetAllCustomersUnmapped();
            
            CustomContractResolver resolver = new CustomContractResolver();

            resolver.IgnoreProperty(typeof(Entity.Models.Order), "Customer");
            resolver.IgnoreProperty(typeof(Entity.Models.OrderItem), "Order");
            resolver.IgnoreProperty(typeof(Entity.Models.Product), "OrderItems");
            resolver.IgnoreProperty(typeof(Entity.Models.Supplier), "Products");

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = resolver
            };

            List<string> list = new List<string>();

            foreach(var c in customers)
            {
                list.Add(JsonConvert.SerializeObject(c, settings));
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
