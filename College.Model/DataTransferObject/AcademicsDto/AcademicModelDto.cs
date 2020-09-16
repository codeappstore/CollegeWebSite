using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.AcademicsDto
{
    public class AcademicModelDto
    {
        public int AcademicId { get; set; }

        [Required] public string Description { get; set; }
    }
}