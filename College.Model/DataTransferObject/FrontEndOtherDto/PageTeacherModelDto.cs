using System.Collections.Generic;
using College.Model.DataTransferObject.PageDto;
using College.Model.DataTransferObject.TeacherDto;

namespace College.Model.DataTransferObject.FrontEndOtherDto
{
    public class PageTeacherModelDto
    {
        public PageModelDto Page { get; set; }
        public IList<TeacherModelDto> Teacher { get; set; }
    }
}