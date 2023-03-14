using Microsoft.AspNetCore.Mvc;

namespace ProductsAPIServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return "Hello from the Products Microservice";
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return $"Customers Microservice - Requested Product ID is {id}";
        }
    }
}
