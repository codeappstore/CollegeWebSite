using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.ImportantLinksDto
{
    public class ImportantLinksModelDto
    {
        public int ImportantLinkId { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Link { get; set; }
    }
}