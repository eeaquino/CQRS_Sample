using CQRS_Sample.Commands.Customers;
using CQRS_Sample.Data.DbContexts;
using CQRS_Sample.Data.Entities.Customers;
using CQRS_Sample.Data.Repositories;

namespace CQRS_Sample.Data.CommandHandlers.Customers
{
    public sealed class ChangeCustomerCompanyHandler : IAsyncCommandHandler<ChangeCustomerCompany>
    {
        private readonly IGenericRepository<Customer,MainDbContext> _customerRepository;
        public ChangeCustomerCompanyHandler(IGenericRepository<Customer, MainDbContext> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CommandHandlerResult> Handle(ChangeCustomerCompany command,CancellationToken ct)
        {
            var result = new CommandHandlerResult();
            var customer = await _customerRepository.GetAsync(g => g.CustomerId == command.CustomerId,ct);
            if (customer == null)
            {
                result.Errors.Add("Customer not found");
                result.Success = false;
                return result;
            }
            customer.Company = command.Company;
            try
            {
                await _customerRepository.SaveAsync(ct);
            }
            catch (Exception ex)
            {
                result.Errors.Add("Unable to save to the database");
                //Log Error ex
                result.Success = false;
            }
            return result;
        }
    }
}
