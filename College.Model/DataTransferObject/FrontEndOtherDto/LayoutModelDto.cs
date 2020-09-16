using System.Collections.Generic;
using College.Model.DataTransferObject.FooterDto;
using College.Model.DataTransferObject.ImportantLinksDto;
using College.Model.DataTransferObject.SalientFeaturesDto;
using College.Model.DataTransferObject.StudentSayStudents;
using College.Model.DataTransferObject.StudentsSayDto;

namespace College.Model.DataTransferObject.FrontEndOtherDto
{
    public class LayoutModelDto
    {
        public FooterUpdateModelDto Footer { get; set; }
        public StudentSayModelDto StudentSay { get; set; }
        public IList<ImportantLinksModelDto> ImportantLinks { get; set; }
        public IList<SalientFeaturesModelDto> SalientFeatures { get; set; }
        public IList<StudentSayStudentsModelDto> Students { get; set; }
    }
}