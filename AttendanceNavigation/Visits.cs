using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceNavigation
{
    public class Visits
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
