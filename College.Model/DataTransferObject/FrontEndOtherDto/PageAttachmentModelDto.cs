using College.Model.DataTransferObject.AttachmentDto;
using College.Model.DataTransferObject.PageDto;

namespace College.Model.DataTransferObject.FrontEndOtherDto
{
    public class PageAttachmentModelDto
    {
        public PageModelDto Page { get; set; }
        public AttachmentModelDto Attachment { get; set; }
    }
}