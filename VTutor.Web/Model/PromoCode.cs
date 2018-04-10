using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTutor.Model
{
    public class PromoCode
    {

		public Guid Id { get; set; }

		public string Name { get; set; }

		public int DiscountAmount { get; set; }

		public int? IndividualUsageAmount { get; set; }

		public int? TotalUsageAmount { get; set; }
    }
}
