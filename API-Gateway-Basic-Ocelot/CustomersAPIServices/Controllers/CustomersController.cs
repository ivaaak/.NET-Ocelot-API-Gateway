using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomersAPIServices.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
    // http://localhost:9001/api/customers/get
        [HttpGet]
        public string Get()
        {
            return "Hello from the Customers Microservice";
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Customers Microservice - Requested Customer ID is {id}";
        }

        [Authorize("permission")]
        [HttpGet]
        [Route("authorizedEndpoint")]
        public string AuthorizedEndpoint()
        {
            return "Customers Microservice - Authorized Endpoint (JWT Bearer Token)";
        }
    }
}
