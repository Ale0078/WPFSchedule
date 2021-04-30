using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPFSchedule.Models;
using WPFSchedule.Models.Enums;

namespace WPFSchedule.Helpers.Parsers
{
    public static class StoragePatternParser
    {
        private const int _daysInWeek = 7;
        private const int _maxDaysInMonth = 31;
        private const int _possibleMonthIndexOptionsNumber = 5;

        public static long GetPatternStorageValue(StoragePattern source)
        {
            (long result, long position) data = (0, 1);

            return data.AddWeekDays(source)
                .AddMonthIndex(source)
                .AddDates(source)
                .AddInterval(source);
        }

        private static (long result, long position) AddWeekDays(this (long result, long index) data, StoragePattern source)
        {
            if (source.DaysOfWeek != null)
            {
                foreach (DayOfWeek item in source.DaysOfWeek)
                {
                    data.result |= data.index << (int)item;
                }
            }

            data.index <<= _daysInWeek;

            return data;
        }

        private static (long result, long index) AddMonthIndex(this (long result, long index) data, StoragePattern source)
        {
            if (source.Index.HasValue)
            {
                data.result |= data.index << (int)source.Index;
            }

            data.index <<= _possibleMonthIndexOptionsNumber;

            return data;
        }

        private static (long result, long index) AddDates(this (long result, long index) data, StoragePattern source)
        {
            if (source.Dates != null)
            {
                foreach (int item in source.Dates)
                {
                    data.result |= data.index << (int)item;
                }
            }

            data.index <<= _maxDaysInMonth + 1;

            return data;
        }

        private static long AddInterval(this (long result, long index) data, StoragePattern source)
        {
            data.result |= data.index << source.Interval;

            return data.result;
        }

        public static StoragePattern GetFullDataFromStorage(long storage)
        {
            string stringRepresentationOfStorage = Convert.ToString(storage, 2);

            int subtractionPart = stringRepresentationOfStorage.Length - _daysInWeek;

            string daysString = stringRepresentationOfStorage
                .Substring(subtractionPart);

            string indexString = stringRepresentationOfStorage
                .Substring(subtractionPart -= _possibleMonthIndexOptionsNumber, _possibleMonthIndexOptionsNumber);

            string datesString = stringRepresentationOfStorage
                .Substring(subtractionPart -= _maxDaysInMonth - 1, _maxDaysInMonth);

            string intervalString = stringRepresentationOfStorage
                .Substring(0, subtractionPart);

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
