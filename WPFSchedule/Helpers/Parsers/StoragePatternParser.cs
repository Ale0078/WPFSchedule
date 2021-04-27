using System;
using System.Collections.Generic;

using WPFSchedule.Models;
using WPFSchedule.Models.Enums;

namespace WPFSchedule.Helpers.Parsers
{
    public static class StoragePatternParser
    {
        private const int DAYS_IN_WEEK = 7;
        private const int MAX_DAYS_IN_WEEK = 31;
        private const int POSSIBLE_MONTH_INDEX_OPTIONS_NUMBER = 5;

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
            long defaultValue = data.index;

            if (source.DaysOfWeek != null)
            {
                foreach (DayOfWeek item in source.DaysOfWeek)
                {
                    data.index <<= (int)item;
                    data.result |= data.index;
                    data.index = defaultValue;
                }
            }

            data.index <<= DAYS_IN_WEEK;

            return data;
        }

        private static (long result, long index) AddMonthIndex(this (long result, long index) data, StoragePattern source)
        {
            if (source.Index.HasValue)
            {
                data.result |= data.index << (int)source.Index;
            }

            data.index <<= POSSIBLE_MONTH_INDEX_OPTIONS_NUMBER;

            return data;
        }

        private static (long result, long index) AddDates(this (long result, long index) data, StoragePattern source)
        {
            long defaultValue = data.index;

            if (source.Dates != null)
            {
                foreach (int item in source.Dates)
                {
                    data.index <<= item;
                    data.result |= data.index;
                    data.index = defaultValue;
                }
            }

            data.index <<= MAX_DAYS_IN_WEEK + 1;

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

            string daysString = stringRepresentationOfStorage
                .Substring(stringRepresentationOfStorage.Length - DAYS_IN_WEEK);

            string indexString = stringRepresentationOfStorage
                .Substring(stringRepresentationOfStorage.Length - DAYS_IN_WEEK - POSSIBLE_MONTH_INDEX_OPTIONS_NUMBER, POSSIBLE_MONTH_INDEX_OPTIONS_NUMBER);

            string datesString = stringRepresentationOfStorage
                .Substring(stringRepresentationOfStorage.Length - DAYS_IN_WEEK - POSSIBLE_MONTH_INDEX_OPTIONS_NUMBER - MAX_DAYS_IN_WEEK - 1, MAX_DAYS_IN_WEEK);

            string intervalString = stringRepresentationOfStorage
                .Substring(0, stringRepresentationOfStorage.Length - DAYS_IN_WEEK - POSSIBLE_MONTH_INDEX_OPTIONS_NUMBER - MAX_DAYS_IN_WEEK - 1);

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
                    datesCollection.Add(MAX_DAYS_IN_WEEK - i);
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
