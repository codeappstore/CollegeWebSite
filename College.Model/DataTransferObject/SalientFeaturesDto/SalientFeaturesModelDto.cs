using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.SalientFeaturesDto
{
    public class SalientFeaturesModelDto
    {
        public int SalientFeatureId { get; set; }

        [Required] public string Feature { get; set; }
    }
}