namespace AirBnbFakeDatabase.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public string Name { get; set; }
        public string HostResponseTime { get; set; }
        public double HostResponseRate { get; set; }
        public int Bathrooms { get; set; }
        public int Bedrooms { get; set; }
        public int Beds { get; set; }
        public string BedType { get; set; }
        public int SquareFeet { get; set; }
        public double Price { get; set; }
        public double PricePerWeek { get; set; }
        public int GuestsIncluded { get; set; }
        public int MinimumNights { get; set; }
        public int MaximumNights { get; set; }
        public int NumberOfReviews { get; set; }
        public string Neighbourhood { get; set; }
    }
}
