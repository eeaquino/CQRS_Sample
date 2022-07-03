using CQRS_Sample.DTOs.Customers;

namespace CQRS_Sample.Queries.Customers
{
    public sealed class GetCustomerById : IQuery<CustomerDto>
    {
        public GetCustomerById(int id)
        {
            Id = id;
        }
        public int Id { get; init; }
    }
}
