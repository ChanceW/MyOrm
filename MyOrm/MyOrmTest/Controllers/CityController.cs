using Microsoft.AspNetCore.Mvc;
using MyOrmTest.Models;
using MyOrmTest.Repos;
using System.Collections.ObjectModel;

namespace MyOrmTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private IWorldRepo _worldRepo;

        public CityController(IWorldRepo worldRepo) 
        {
            _worldRepo = worldRepo;
        }
        
        [HttpGet]
        public Collection<City> Get()
        {
            return _worldRepo.GetCities();
        }
    }
}
