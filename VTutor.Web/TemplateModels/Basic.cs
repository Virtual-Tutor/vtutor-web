using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTutor.Email.TemplateModels
{
    public class Basic : ITemplateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
