using System;
using System.Collections.Generic;

namespace VTutor.Model
{
  public partial class Student
  {
    public Guid Id { get; set; }


    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string ParentsFirstName { get; set; }
    public string ParentsLastName { get; set; }
    public string ParentsEmailAddress { get; set; }

    public string PhoneNumber { get; set; }
    public string StreetAddress { get; set; }

    public string City { get; set; }
    public string State { get; set; }

    public string Zip { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    public ApplicationUser User { get; set; }

    public List<Appointment> Appointments { get; set; }

  }
}
