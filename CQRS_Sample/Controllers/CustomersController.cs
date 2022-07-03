using CQRS_Sample.Data.CommandHandlers;
using CQRS_Sample.DTOs.Customers;
using CQRS_Sample.Services.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("[action]/{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int customerId,CancellationToken ct)
        {
            var customer = await _customerService.GetCustomerById(customerId,ct);
            if (customer == null) return NoContent();
            return customer;
        }

        [HttpPut("[action]/{customerId}")]
        public async Task<ActionResult<CommandHandlerResult>> ChangeCustomerCompany(int customerId,string company, CancellationToken ct)
        {
            var result = await _customerService.ChangeCustomerCompany(customerId,company, ct);
            return result;
        }
    }
}
