namespace PrintPress.Data
{
    public struct Address
    {
        public string HouseNameOrNumber { get; init; } = string.Empty;
        public string Road { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string County { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public string Postcode { get; init; } = string.Empty;

        public Address(
            string houseName,
            string road,
            string city,
            string county,
            string country,
            string postcode)
        {
            HouseNameOrNumber = houseName;
            Road = road;
            City = city;
            County = county;
            Country = country;
            Postcode = postcode;
        }
    }
}