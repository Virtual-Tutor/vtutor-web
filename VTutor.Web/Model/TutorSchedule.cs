using System;
using System.Collections.Generic;

namespace VTutor.Model
{
    public partial class TutorSchedule
    {
        public Guid Id { get; set; }
        public Tutor Tutor { get; set; }
    }
}
