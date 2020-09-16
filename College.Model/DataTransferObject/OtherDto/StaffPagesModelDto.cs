using System.Collections.Generic;
using College.Model.DataTransferObject.PageDto;
using College.Model.DataTransferObject.TeacherDto;

namespace College.Model.DataTransferObject.OtherDto
{
    public class StaffPagesModelDto
    {
        public PageModelDto Page { get; set; }
        public IList<TeacherModelDto> Staff { get; set; }
    }
}