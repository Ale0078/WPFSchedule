using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFSchedule.Models;
using WPFSchedule.Views;

namespace WPFSchedule.ViewModels
{
    class ScheduleCreatorViewModel : INotifyPropertyChanged
    {
        public ScheduleCreatorViewModel()
        {
            StartTimeList = new List<TimeSpan>();
            StartTimeList = FillTimeList(StartTimeList);

            EventOccurrenceList = new List<string>()
            {
                "Every day",
                "Every week",
                "Every month",
                "Don't repeat",
                "Another ..."
            };
        }

        //private TimeSpan _startTime;
        //private TimeSpan _endTiime;
        private int _chosedInterval;

        public DateTime ChosedDate { get; set; } = DateTime.Today;
        public string Caption { get; set; }

        public int ChosedInterval
        {
            get => _chosedInterval;
            set
            {
                if (value == 4)
                {
                    //_chosedInterval = value;
                    AdditionalCreationOptionView additionalOption = new AdditionalCreationOptionView();

                    additionalOption.ShowDialog();
                }

                _chosedInterval = value;
            }
        }

        public List<TimeSpan> StartTimeList { get; }
        public List<string> EventOccurrenceList { get; }

        private List<TimeSpan> FillTimeList(List<TimeSpan> _startTimeList)
        {
            int timeMinutes = 0;

            for (int i = 0; i < 96; i++)
            {
                _startTimeList.Add(TimeSpan.FromMinutes(timeMinutes));

                timeMinutes += 15;
            }

            return _startTimeList;
        }

        public string DisplayedEventOccurrence
        {
            get => EventOccurrenceList[3];
        }

        //public TimeSpan StartTime
        //{
        //    get => StartTimeList.First(t => t.TotalHours == 10);
        //    set
        //    {
        //        _startTime = value;
        //    }
        //}

        //public TimeSpan EndTime
        //{
        //    get => StartTimeList.First(t => t.TotalHours == 12);
        //    set => _endTiime = value;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
