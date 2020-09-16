using System.Collections.Generic;
using College.Model.DataTransferObject.AcademicItemsDto;
using College.Model.DataTransferObject.AcademicsDto;
using College.Model.DataTransferObject.AttachmentDto;
using College.Model.DataTransferObject.CarouselDto;
using College.Model.DataTransferObject.PageDto;

namespace College.Model.DataTransferObject.OtherDto
{
    public class PageCarouselAcademicItemsModelDto
    {
        public IList<CarouselModelDto> Carousel { get; set; }
        public IList<AcademicItemsModelDto> AcademicItems { get; set; }
        public AcademicModelDto Academic { get; set; }
        public PageModelDto Page { get; set; }
        public AttachmentModelDto Brochure { get; set; }
    }
}