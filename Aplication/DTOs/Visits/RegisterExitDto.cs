using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.DTOs.Visits
{
    public class RegisterExitDto
    {
        public Guid? VisitId { get; set; }
        public string? Code { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
