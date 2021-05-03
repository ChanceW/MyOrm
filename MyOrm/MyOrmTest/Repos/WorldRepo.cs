using MyOrmTest.Models;
using MyOrmTest.Repos;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyOrm.Repos
{
    public class WorldRepo : MySqlRepo, IWorldRepo
    {
        public WorldRepo() : base("server=127.0.0.1;uid=root;pwd=admin;database=world;") { 
        }
        public Collection<City> GetCities() {
            string proc = "get_Cities";
            var procParams = new Dictionary<string, object>();
            procParams.Add("_Id", null);
            return Proc<City>(proc, cityColumnMapping, procParams);
        }

        private Dictionary<string, string> cityColumnMapping = new Dictionary<string, string> {
            { "Id", "Id" },
            { "Name", "Name" },
            { "Population", "Population" },
            { "CountryCode", "CountryCode" },
            { "District", "District" }
        };
    }
}
