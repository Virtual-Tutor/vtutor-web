using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VTutor.Model
{
    public partial class DatabaseLog
    {
		
		public Guid Id { get; set; }

		public DateTime Date { get; set; }

		public string LogMessage { get; set; }

		public bool Error { get; set; }

    }
}
