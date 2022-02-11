using NMS.WEB.Models.Enum;
using System;

namespace NMS.WEB.Models.ViewModel
{
    public class VmNote
    {
        public Guid Id { get; set; }
        public NoteType NoteType { get; set; }
        public NoteStatus NoteStatus { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public string Url { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
