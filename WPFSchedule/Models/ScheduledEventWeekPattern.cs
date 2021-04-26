using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSchedule.Models
{
    public class ScheduledEventWeekPattern
    {
        public ScheduledEvent Event { get; set; }

        public StoragePattern Storage { get; set; }
    }
}
