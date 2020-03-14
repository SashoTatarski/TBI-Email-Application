using EMS.WebProject.Models.Applications;
using System.Collections.Generic;

namespace EMS.WebProject.Models.Emails
{
    public class PreviewViewModel
    {
        public string Id { get; set; }

        public string DateReceived { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string EmailBody { get; set; }

        public string Subject { get; set; }

        public string Status { get; set; }

        public InputViewModel InputViewModel { get; set; } = new InputViewModel();

        public List<AttachmentViewModel> Attachments { get; set; }

        public AppPreviewViewModel Application { get; set; } = new AppPreviewViewModel();
    }
}
