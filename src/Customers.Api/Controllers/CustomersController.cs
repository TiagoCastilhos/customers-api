using Customers.Application.Abstractions.Services;
using Customers.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers;

[ApiController]
[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomersController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(CustomerOutputModel[]), 200, contentType: "application/json")]
    public IActionResult Get()
    {
        return Ok(_customersService.GetAll());
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(CustomerOutputModel), 200, contentType: "application/json")]
    [ProducesResponseType(typeof(string), 400, contentType: "text/plain")]
    public async Task<IActionResult> GetAsync(string email)
    {
        return Ok(await _customersService.GetAsync(email));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(CustomerOutputModel), 200, contentType: "application/json")]
    [ProducesResponseType(typeof(string), 400, contentType: "text/plain")]
    public async Task<IActionResult> PostAsync([FromBody] CustomerInputModel customerInputModel)
    {
        return Ok(await _customersService.InsertAsync(customerInputModel));
    }

    [HttpPut("{email}")]
    [ProducesResponseType(typeof(CustomerOutputModel), 200, contentType: "application/json")]
    [ProducesResponseType(typeof(string), 400, contentType: "text/plain")]
    public async Task<IActionResult> PutAsync(string email, [FromBody] CustomerInputModel customerInputModel)
    {
        await _customersService.UpdateAsync(customerInputModel);
        return Ok();
    }

    [HttpDelete("{email}")]
    [ProducesResponseType(typeof(CustomerOutputModel), 200, contentType: "application/json")]
    [ProducesResponseType(typeof(string), 400, contentType: "text/plain")]
    public async Task<IActionResult> DeleteAsync(string email)
    {
        await _customersService.DeleteAsync(email);
        return Ok();
    }
}
