namespace CollegeWebsite.DataAccess.Models.Pages.Dtos
{
    public class StaticPagesReadDto
    {
        public string PageId { get; set; }
        public string PageTitle { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string BackgroundImage { get; set; }
    }
}
