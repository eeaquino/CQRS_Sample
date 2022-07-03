using CQRS_Sample.Data.DbContexts;
using CQRS_Sample.DTOs.Customers;
using CQRS_Sample.Queries.Customers;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Sample.Data.QueryHandlers.Customers
{
    public sealed class GetCustomerByIdHandler : IAsyncQueryHandler<GetCustomerById, CustomerDto>
    {
        public MainDbContext _context;

        public GetCustomerByIdHandler(MainDbContext context)
        {
            _context = context;
        }
        public async Task<CustomerDto?> Handle(GetCustomerById query,CancellationToken ct)
        {
            var customer =await  _context.Customers
                .Where(w => w.CustomerId == query.Id)
                .Select(s=>new CustomerDto()
                {
                    Id = s.CustomerId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    Phone = s.Phone,
                    Address = s.Address,
                    City = s.City,
                    State = s.State,
                    PostalCode = s.PostalCode,
                    Country = s.Country,
                    Company = s.Company,
                    Fax = s.Fax
                })
                .SingleOrDefaultAsync(ct);
            return customer;
        }

    }
}
