using AirBnbFakeDatabase.Models;
using RandomNameGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirBnbFakeDatabase.Database
{
    public class DatabaseSeed
    {
        private readonly Random _random;
        private readonly List<string> _bedTypes;
        private readonly List<string> _hostResponseTimes;
        private readonly List<string> _neighbourhoods;

        public DatabaseSeed()
        {
            _random = new Random();
            _bedTypes = new List<string> { "Real Bed", "Couch", "Real Bed", "Futon", "Real Bed", "Pull-out sofa", "Real Bed" };
            _hostResponseTimes = new List<string> { "an hour", "a day", "a few hours", "a few days", "a few days or more", "N/A" };
            _neighbourhoods = new List<string> { "Bijlmer-Centrum", "Bijlmer-Oost", "Bos en Lommer", "Buitenveldert - Zuidas", " Slotervaart" };
        }

        public IEnumerable<Listing> GenerateFakeData(int amount)
        {
            var listings = new List<Listing>();

            for (int i = 0; i < amount; i++)
            {
                double price = RandomNumber(100, 400);
                listings.Add(new Listing
                {
                    ListingId = i,
                    BedType = RandomBedType(),
                    GuestsIncluded = RandomNumber(1, 3),
                    HostResponseRate = RandomNumber(1, 100),
                    HostResponseTime = RandomHostResponseTime(),
                    MaximumNights = RandomNumber(1, 31),
                    MinimumNights = RandomNumber(1, 5),
                    Name = GenerateName(RandomBool()),
                    Price = price,
                    PricePerWeek = RandomNumber(600, 3500),
                    Bathrooms = RandomNumberAccordingToPrice(1, 5, price),
                    Bedrooms = RandomNumberAccordingToPrice(1, 5, price),
                    Beds = RandomNumberAccordingToPrice(1, 5, price),
                    NumberOfReviews = RandomNumber(0, 200),
                    SquareFeet = RandomNumberAccordingToPrice(5, 100, price),
                    Neighbourhood = RandomNeighbourhood()
                });
            }

            return listings;
        }

        private int RandomNumberAccordingToPrice(int min, int max, double price)
        {
            return RandomNumber(min, (int)Math.Ceiling(max / (price / 100)));
        }

        private bool RandomBool()
        {
            return _random.NextDouble() >= 0.5;
        }

        private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        private string RandomBedType()
        {
            int index = RandomNumber(0, _bedTypes.Count);
            return _bedTypes[index];
        }

        private string RandomHostResponseTime()
        {
            int index = RandomNumber(0, _hostResponseTimes.Count);
            return _hostResponseTimes[index];
        }

        private string RandomNeighbourhood()
        {
            int index = RandomNumber(0, _neighbourhoods.Count);
            return _neighbourhoods[index];
        }

        private string GenerateName(bool isMale)
        {
            var gender = isMale ? Gender.Male : Gender.Female;

            string name = NameGenerator.GenerateFirstName(gender);
            
            name = name.ToLower();
            return name.First().ToString().ToUpper() + name.Substring(1);
        }
    }
}
