using System;

namespace WPFSchedule.Models
{
    public class EventTime
    {
        public DateTime EventStart { get; set; }
        public DateTime EventFinish { get; set; }

        public TimeSpan EventTimeStart { get; set; }
        public TimeSpan EventTimeFinish { get; set; }
    }
}
