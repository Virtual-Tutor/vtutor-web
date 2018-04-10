using System;
using System.Collections.Generic;

namespace VTutor.Model
{
    public partial class Tutor
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Biography { get; set; }
		public string Education { get; set; }
		public string Languages { get; set; }
		public string Hobbies { get; set; }

		public Image TutorImage { get; set; }
		public List<Image> Documents { get; set; }

        public ApplicationUser User { get; set; }

		public List<ScheduleBlock> AvailabilitySchedule { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}

