using CQRS_Sample.Data.CommandHandlers;
using CQRS_Sample.Data.Entities.Customers;
using CQRS_Sample.DTOs.Customers;

namespace CQRS_Sample.Services.Customers
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerById(int customerId, CancellationToken token);
        Task<CommandHandlerResult> ChangeCustomerCompany(int customerId, string company, CancellationToken ct);
    }
}