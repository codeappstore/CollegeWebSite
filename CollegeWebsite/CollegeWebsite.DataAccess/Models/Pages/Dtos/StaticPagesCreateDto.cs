namespace CollegeWebsite.DataAccess.Models.Pages.Dtos
{
    public class StaticPagesCreateDto
    {
        public string PageTitle { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string BackgroundImage { get; set; }
    }
}
