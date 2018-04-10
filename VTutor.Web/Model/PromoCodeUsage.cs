using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTutor.Model
{
    public class PromoCodeUsage
    {

		public Guid Id { get; set; }
		public Student Student { get; set; }
		public PromoCode PromoCode { get; set; }
		public DateTime UsedDate { get; set; }

    }
}
