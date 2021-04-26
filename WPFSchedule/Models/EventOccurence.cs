using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using WPFSchedule.Models.Enums;

namespace WPFSchedule.Models
{
    public class EventOccurence : INotifyPropertyChanged
    {
        private DateTime eventStart;
        private DateTime eventFinish;
        private PatternType pattern;
        private long storage;

        public long Id { get; set; }
        public long? StudentGroubId { get; set; }

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

        public PatternType Pattern 
        {
            get 
            {
                return pattern;
            }
            set 
            {
                pattern = value;

                OnPropertyChanged("Pattern");
            }
        }

        public long Storage 
        {
            get 
            {
                return storage;
            }
            set 
            {
                storage = value;

                OnPropertyChanged("Storage");
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
