namespace PrintPress.Data.Builder
{
    public class AddressBuilder
    {
        public string HouseNameOrNumber { get; set; } = string.Empty;
        public string Road { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;

        public Address ToAddress()
        {
            return new Address
                (HouseNameOrNumber,
                Road, 
                City, 
                County, 
                Country, 
                Postcode);
        }
    }
}