using MyOrmTest.Models;
using System.Collections.ObjectModel;

namespace MyOrmTest.Repos
{
    public interface IWorldRepo
    {
        public Collection<City> GetCities();
    }
}
