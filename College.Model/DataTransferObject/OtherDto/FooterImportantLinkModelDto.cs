using System.Collections.Generic;
using College.Model.DataTransferObject.FooterDto;
using College.Model.DataTransferObject.ImportantLinksDto;

namespace College.Model.DataTransferObject.OtherDto
{
    public class FooterImportantLinkModelDto
    {
        public FooterUpdateModelDto FooterUpdateModel { get; set; }
        public IList<ImportantLinksModelDto> ImportantLinksModel { get; set; }
    }
}