namespace College.Model.DataTransferObject.EmailExtraDto
{
    public class ButtonedEmailModelDto : NormalEmailModelDto
    {
        public string ButtonUrl { get; set; }
        public string ButtonText { get; set; }
        public string BoodyWarningMessage { get; set; }
    }
}