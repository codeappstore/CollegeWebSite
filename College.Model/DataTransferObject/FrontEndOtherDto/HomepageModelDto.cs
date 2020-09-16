using System.Collections.Generic;
using College.Model.DataTransferObject.AcademicItemsDto;
using College.Model.DataTransferObject.AcademicsDto;
using College.Model.DataTransferObject.CarouselDto;
using College.Model.DataTransferObject.PageDto;
using College.Model.DataTransferObject.PopupDto;

namespace College.Model.DataTransferObject.FrontEndOtherDto
{
    public class HomepageModelDto
    {
        public PageModelDto Page { get; set; }
        public PopupModelDto PopUp { get; set; }
        public AcademicModelDto Academic { get; set; }
        public IList<AcademicItemsModelDto> AcademicItems { get; set; }
        public IList<CarouselModelDto> Carousel { get; set; }
    }
}