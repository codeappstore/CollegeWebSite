using System;

namespace College.Model.Models
{
    public class FooterHeaderModel
    {
        public int FooterHeaderId { get; set; }
        public string Slogan { get; set; }

        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactAddress { get; set; }

        public string FacebookLink { get; set; }
        public string TweeterLink { get; set; }
        public string InstaGramLink { get; set; }
        public string GooglePlusLink { get; set; }
        public string YoutubeLink { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}