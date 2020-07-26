namespace CollegeWebsite.DataAccess.Models.Miscellaneous.Dtos
{
    public class AccessDetailsDto
    {
        public string AuthId { get; set; }
        public string IpAddress { get; set; }
        public string ContinentCode { get; set; }
        public string CountryName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string AccessAgent { get; set; }
    }
}
