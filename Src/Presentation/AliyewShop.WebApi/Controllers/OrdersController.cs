using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    private IOrderRepository _orderRepository {  get; set; }
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<OrdersController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<OrdersController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string value)
    {
        return StatusCode(201);
    }

    // PUT api/<OrdersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<OrdersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
