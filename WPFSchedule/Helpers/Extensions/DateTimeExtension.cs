using System;

namespace WPFSchedule.Helpers.Extensions
{
    public static class DateTimeExtension
    {
        public const int DAYS_IN_WEEK = 7;

        public static (DateTime startWeek, DateTime endWeek) GetStartAndEndWeek(this DateTime selectedDate) 
        {
            return (selectedDate.AddDays(-(int)selectedDate.DayOfWeek)
                , selectedDate.AddDays(DAYS_IN_WEEK - (int)selectedDate.DayOfWeek - 1));
        }

        public static void NextWeek(this DateTime selectedDate) 
        {
            selectedDate = selectedDate.AddDays(DAYS_IN_WEEK);
        }

        public static void PreviousWeek(this DateTime selectedDate) 
        {
            selectedDate = selectedDate.AddDays(-DAYS_IN_WEEK);
        }
    }
}
