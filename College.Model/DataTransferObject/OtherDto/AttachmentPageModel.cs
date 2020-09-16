using College.Model.DataTransferObject.AttachmentDto;
using College.Model.DataTransferObject.PageDto;

namespace College.Model.DataTransferObject.OtherDto
{
    public class AttachmentPageModel
    {
        public AttachmentModelDto Attachment { get; set; }
        public PageModelDto Page { get; set; }
    }
}