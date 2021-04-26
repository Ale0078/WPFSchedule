using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFSchedule.Models
{
    public class ScheduledEvent : INotifyPropertyChanged
    {
        private DateTime eventStart;
        private DateTime eventFinish;

        public long Id { get; set; }
        public long EventOccurenceId { get; set; }
        public long? StudentGroupId { get; set; }
        public long? ThemeId { get; set; }
        public long? MentorId { get; set; }
        public long? LessonId { get; set; }

        public DateTime EventStart 
        {
            get
            {
                return eventStart;   
            }
            set 
            {
                eventStart = value;

                OnPropertyChanged("EventStart");
            }
        }

        public DateTime EventFinish 
        {
            get 
            {
                return eventFinish;
            }
            set 
            {
                eventFinish = value;

                OnPropertyChanged("EventFinish");
            }
        }

        #region ===================== Notifier ========================
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
