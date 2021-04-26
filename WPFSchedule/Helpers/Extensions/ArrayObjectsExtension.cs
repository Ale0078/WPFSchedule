using System;

using WPFSchedule.Models;

namespace WPFSchedule.Helpers.Extensions
{
    public static class ArrayObjectsExtension
    {
        public static ScheduledEvent GetScheduledEvent(this object[] parametrs) 
        {
            ScheduledEvent scheduled = new ScheduledEvent();

            for (int i = 0; i < parametrs.Length; i++)
            {
                switch (i) 
                {
                    case 0:
                        scheduled.Id = (long)parametrs[i];
                        break;
                    case 1:
                        scheduled.EventOccurenceId = (long)parametrs[i];
                        break;
                    case 2:
                        scheduled.StudentGroupId = ConvertToLongOrNull(parametrs[i]);
                        break;
                    case 3:
                        scheduled.ThemeId = ConvertToLongOrNull(parametrs[i]);
                        break;
                    case 4:
                        scheduled.MentorId = ConvertToLongOrNull(parametrs[i]);
                        break;
                    case 5:
                        scheduled.LessonId = ConvertToLongOrNull(parametrs[i]);
                        break;
                    case 6:
                        scheduled.EventStart = (DateTime)parametrs[i];
                        break;
                    case 7:
                        scheduled.EventFinish = (DateTime)parametrs[i];
                        break;
                }
            }

            return scheduled;
        }

        private static long? ConvertToLongOrNull(object valueToConvert) 
        {
            if (valueToConvert is DBNull)
            {
                return null;
            }
            
            return (long)valueToConvert;            
        }
    }
}
