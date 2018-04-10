using System;
using System.Collections.Generic;

namespace VTutor.Model
{
    public partial class Appointment
    {
        public Guid Id { get; set; }
        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Student Student { get; set; }
        public Tutor Tutor { get; set; }
    }
}
