using AirBnbFakeDatabase.Models;
using System.Collections.Generic;

namespace AirBnbFakeDatabase.Database
{
    public class FakeDatabase
    {
        public IEnumerable<Listing> Listings { get; set; }

        public FakeDatabase()
        {
            var seeder = new DatabaseSeed();
            Listings = seeder.GenerateFakeData(200);
        }
    }
}
