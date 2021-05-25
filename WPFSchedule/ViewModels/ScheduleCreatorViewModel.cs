using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            frameVisibility = Visibility.Collapsed;
            pageVisibility = Visibility.Visible;
        }

        private TimeSpan _startTime;
        private TimeSpan _endTiime;
        private int _chosedInterval;
        private Visibility frameVisibility;
        private Visibility pageVisibility;

        public DateTime ChosedDate { get; set; } = DateTime.Today;
        public string Caption { get; set; }

        public Visibility FrameVisibility
        {
            get => frameVisibility;
            set
            {
                frameVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility PageVisibility
        {
            get => pageVisibility;
            set
            {
                pageVisibility = value;
                OnPropertyChanged();
            }
        }

        public int ChosedInterval
        {
            get => _chosedInterval;
            set
            {
                Caption = "123";
                OnPropertyChanged();

                if (value == 4)
                {
                    PageVisibility = Visibility.Collapsed;
                    FrameVisibility = Visibility.Visible;
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

        public TimeSpan StartTime
        {
            get => StartTimeList.First(t => t.TotalHours == 10);
            set
            {
                _startTime = value;
            }
        }

        public TimeSpan EndTime
        {
            get => StartTimeList.First(t => t.TotalHours == 12);
            set => _endTiime = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
