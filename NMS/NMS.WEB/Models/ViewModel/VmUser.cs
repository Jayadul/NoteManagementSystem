using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.WEB.Models.ViewModel
{
    public class VmUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
