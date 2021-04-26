using System;
using System.Collections.Generic;

using WPFSchedule.Models.Enums;

namespace WPFSchedule.Models
{
    public class StoragePattern
    {
        public PatternType Type { get; set; }

        public int Interval { get; set; }

        public IList<DayOfWeek> DaysOfWeek { get; set; }

        public MonthIndex? Index { get; set; }

        public IList<int> Dates { get; set; }
    }
}
