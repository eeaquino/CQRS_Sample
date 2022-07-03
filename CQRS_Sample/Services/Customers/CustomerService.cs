using CQRS_Sample.Commands.Customers;
using CQRS_Sample.Data.CommandHandlers;
using CQRS_Sample.DTOs.Customers;
using CQRS_Sample.Queries.Customers;

namespace CQRS_Sample.Services.Customers
{
    public class CustomerService : IAPIService, ICustomerService
    {
        private readonly IMediator _mediator;

        public CustomerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<CommandHandlerResult> ChangeCustomerCompany(int customerId, string company, CancellationToken ct)
        {
            var command = new ChangeCustomerCompany(customerId, company);
            return _mediator.RunAsync(command, ct);
        }

        public async Task<CustomerDto?> GetCustomerById(int customerId, CancellationToken token)
        {
            var query = new GetCustomerById(customerId);
            return await _mediator.RunAsync(query, token);
        }
    }
}
