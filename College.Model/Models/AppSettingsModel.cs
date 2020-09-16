namespace College.Model.Models
{
    public class AppSettingsModel
    {
        public int SettingsId { get; set; }
        public string ClientCode { get; set; }
        public string ProjectCode { get; set; }
        public string Certificate { get; set; }
        public string License { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
    }
}