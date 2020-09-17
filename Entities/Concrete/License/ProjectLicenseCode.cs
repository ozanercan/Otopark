using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.License
{
    public class ProjectLicenseCode
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string LicenseCode { get; set; }
        public DateTime StartedDateTime { get; set; }
        public DateTime FinishedDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
