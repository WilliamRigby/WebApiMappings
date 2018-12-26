using AutoMapper;
using BusinessLayer.Models;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CustomerService
    {
        private readonly IMapper _mapper;

        public CustomerService()
        {
            _mapper = new AutoMapperConfiguration().Configure().CreateMapper();            
        }

        public async Task<IEnumerable<Entity.Models.Customer>> GetAllCustomersUnmapped()
        {
            IEnumerable<Entity.Models.Customer> customers = null;

            using(CompanyDbContext context = new CompanyDbContext())
            {                
                customers = await context.Customer.Include(c => c.Orders)
                        .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Supplier)
                        .ToListAsync();                
            }

            return customers;
        }      
        
        public async Task<IEnumerable<Customer>> GetAllCustomersMapped()
        {
            IEnumerable<Entity.Models.Customer> customers = null;

            using(CompanyDbContext context = new CompanyDbContext())
            {
                customers = await context.Customer.Include(c => c.Orders)
                        .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Supplier)
                        .ToListAsync();                
            }

            return _mapper.Map<IEnumerable<Customer>>(customers);
        } 
    }
}
