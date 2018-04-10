using System;
using System.Collections.Generic;

namespace VTutor.Model
{
    public partial class ScheduleBlock
    {
        public Guid Id { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public Tutor Tutor { get; set; }
    }
}
