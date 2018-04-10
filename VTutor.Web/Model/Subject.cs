using System;
using System.Collections.Generic;

namespace VTutor.Model
{
    public partial class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Grade SubjectGrade { get; set; }

    }
}
