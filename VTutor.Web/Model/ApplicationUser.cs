using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTutor.Model
{
	public class ApplicationUser : IdentityUser
	{

		public Tutor Tutor { get; set; }

	}
}
