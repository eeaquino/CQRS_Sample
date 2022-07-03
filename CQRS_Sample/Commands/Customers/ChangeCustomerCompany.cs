namespace CQRS_Sample.Commands.Customers
{
    public sealed class ChangeCustomerCompany : ICommand
    {
        public int CustomerId { get; init; }
        public string Company { get; init; }

        public ChangeCustomerCompany(int customerId, string company)
        {
            CustomerId = customerId;
            Company = company;
        }
        
       
    }
}
