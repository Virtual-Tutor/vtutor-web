using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTutor.Email.TemplateModels
{
    public interface ITemplateModel
    {
        string Name { get; set; }
        string Email { get; set; }
    }
}
