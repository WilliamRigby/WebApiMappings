using System;
using Entity;
using EntityModels;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        private GenericRepository<Customer> customerRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<OrderItem> orderItemRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<Supplier> supplierRepository;
        

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(_context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(_context);
                }
                return orderRepository;
            }
        }

        public GenericRepository<OrderItem> OrderItemRepository
        {
            get
            {
                if (this.orderItemRepository == null)
                {
                    this.orderItemRepository = new GenericRepository<OrderItem>(_context);
                }
                return orderItemRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(_context);
                }
                return productRepository;
            }
        }

        public GenericRepository<Supplier> SupplierRepository
        {
            get
            {
                if (this.supplierRepository == null)
                {
                    this.supplierRepository = new GenericRepository<Supplier>(_context);
                }
                return supplierRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
