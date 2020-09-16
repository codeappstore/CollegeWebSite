using System;

namespace College.Model.Models
{
    public class SalientFeatures
    {
        public int SalientFeatureId { get; set; }
        public string Feature { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}