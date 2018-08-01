using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turners.UserPortal.Domain
{
    public class User
    {
        public string Name { get; set; }

        [DisplayName("Job Description")]
        public string JobDescription { get; set; }

        [DisplayName("Telephone Number")]
        public string TelephoneNumber { get; set; }

        public string Extension { get; set; }

        public string Mobile { get; set; }

        public string Department { get; set; }
    }
}
