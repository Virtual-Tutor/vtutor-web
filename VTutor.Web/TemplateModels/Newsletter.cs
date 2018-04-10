using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTutor.Email.TemplateModels
{

    public class BibView
    {
        public string title { get; set; }

        public string reference { get; set; }

        public string articleId { get; set; }
    }

    public class Newsletter
    {

        public string firstName { get; set; }

        public string lastName { get; set; }
        
        public string userId { get; set; }



        public BibView[] bibs { get; set; }

    }
}
