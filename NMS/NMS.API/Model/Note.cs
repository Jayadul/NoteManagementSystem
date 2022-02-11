using NMS.API.Model.Enum;
using NMS.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.Model
{
    public class Note
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
