using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Validations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        // TODO check validator of custom validation
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerDTO> _customerValidator;
        private readonly IValidator<CustomerUpdateRequest> _customerUpdateValidator;
        public CustomerController(ICustomerService customerService,
                IValidator<CustomerDTO> customerValidator,
                IValidator<CustomerUpdateRequest> customerUpdateValidator)
        {
            _customerService = customerService;
            _customerValidator = customerValidator;
            _customerUpdateValidator = customerUpdateValidator;

        }
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] CustomerDTO customerDTO)
        {
            var result = _customerValidator.Validate(customerDTO);
            if (!result.IsValid)
                return BadRequest("Add customer failed");
            var customer = await _customerService.AddAsync(customerDTO);

            if (customer.Succeeded)
            {
                return Ok(customer);
            }
            return BadRequest(customer);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _customerService.GetAsync(pageIndex, pageSize);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put([FromBody] CustomerUpdateRequest request)
        {
            var result = _customerUpdateValidator.Validate(request);
            if (!result.IsValid)
                return BadRequest("Update customer failed");
            var customer = await _customerService.UpdateAsync(request);
            if (customer.Succeeded)
            {
                return Ok(customer);
            }
            return BadRequest(customer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetByIdAsync(int id)
        {
            var result = await _customerService.GetByIdAsync(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var result = await _customerService.DeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}