using AirBnbFakeDatabase.Models;
using System.Collections.Generic;

namespace AirBnbFakeDatabase.Database
{
    public class FakeDatabase
    {
        private static FakeDatabase _fakeDatabase;
        public static FakeDatabase Instance
        {
            get
            {
                if (_fakeDatabase == null)
                    _fakeDatabase = new FakeDatabase();

                return _fakeDatabase;
            }
        }

        public IEnumerable<Listing> Listings { get; set; }

        public FakeDatabase()
        {
            var seeder = new DatabaseSeed();
            Listings = seeder.GenerateFakeData(200);
        }
    }
}
