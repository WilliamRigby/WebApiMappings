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

        public async Task<IEnumerable<EntityModels.Customer>> GetAllCustomersUnmapped()
        {
            IEnumerable<EntityModels.Customer> customers = null;

            using(DatabaseContext context = new DatabaseContext())
            {
                customers = await context.Customer.Include(c => c.Order)
                        .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Supplier)
                        .ToListAsync();                
            }

            return customers;
        }      
        
        public async Task<IEnumerable<Customer>> GetAllCustomersMapped()
        {
            IEnumerable<EntityModels.Customer> customers = null;

            using(DatabaseContext context = new DatabaseContext())
            {
                customers = await context.Customer.Include(c => c.Order)
                        .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Supplier)
                        .ToListAsync();                
            }

            return _mapper.Map<IEnumerable<Customer>>(customers);
        } 
    }
}
