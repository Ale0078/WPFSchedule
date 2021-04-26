using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPFSchedule.Models;
using WPFSchedule.Models.Enums;

namespace WPFSchedule.Helpers.Parsers
{
    public class StoragePatternParser
    {
        private const int _daysInWeek = 7;
        private const int _maxDaysInMonth = 31;
        private const int _possibleMonthIndexOptionsNumber = 5;

        public static StoragePattern GetFullDataFromStorage(long source)
        {
            string stringRepresentationOfStorage = Convert.ToString(source, 2);

            string daysString = stringRepresentationOfStorage
                .Substring(stringRepresentationOfStorage.Length - _daysInWeek);

            string indexString = stringRepresentationOfStorage
                .Substring(stringRepresentationOfStorage.Length - _daysInWeek - _possibleMonthIndexOptionsNumber, _possibleMonthIndexOptionsNumber);

            string datesString = stringRepresentationOfStorage
                .Substring(stringRepresentationOfStorage.Length - _daysInWeek - _possibleMonthIndexOptionsNumber - _maxDaysInMonth - 1, _maxDaysInMonth);

            string intervalString = stringRepresentationOfStorage
                .Substring(0, stringRepresentationOfStorage.Length - _daysInWeek - _possibleMonthIndexOptionsNumber - _maxDaysInMonth - 1);

            return new StoragePattern
            {
                Interval = GetInterval(intervalString),
                Index = GetMonthIndex(indexString),
                Dates = GetDatesList(datesString).Count > 0 ? GetDatesList(datesString) : null,
                DaysOfWeek = GetDaysList(daysString).Count > 0 ? GetDaysList(daysString) : null
            };
        }

        private static IList<DayOfWeek> GetDaysList(string daysString)
        {
            IList<DayOfWeek> daysCollection = new List<DayOfWeek>();

            for (int i = daysString.Length - 1; i >= 0; i--)
            {
                if (daysString[i] == '1')
                {
                    daysCollection.Add((DayOfWeek)(daysString.Length - i - 1));
                }
            }

            return daysCollection;
        }

        private static MonthIndex GetMonthIndex(string indexString)
        {
            return (MonthIndex)(indexString.Length - indexString.LastIndexOf('1') - 1);
        }

        private static IList<int> GetDatesList(string datesString)
        {
            IList<int> datesCollection = new List<int>();

            for (int i = datesString.Length - 1; i >= 0; i--)
            {
                if (datesString[i] == '1')
                {
                    datesCollection.Add(_maxDaysInMonth - i);
                }
            }

            return datesCollection;
        }

        private static int GetInterval(string intervalString)
        {
            return intervalString.Length - intervalString.LastIndexOf('1') - 1;
        }
    }
}
