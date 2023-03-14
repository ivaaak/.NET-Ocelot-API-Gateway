using Faker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPIServices.Controllers;

[Route("api/[controller]")]
public class CustomersController : Controller
{
    [Authorize]
    [HttpGet]
    public IEnumerable<string> Get()
    {
        // generate fake customer data / names
        return new[] 
        {
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}", 
            $"{Faker.Name.FullName(NameFormats.WithPrefix)}"
        };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return $"A customer with the ID - {id}";
    }
}