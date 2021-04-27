using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

using WPFSchedule.Models.Commands;
using WPFSchedule.Models;
using WPFSchedule.Helpers.SqlQueries;
using WPFSchedule.Helpers.Extensions;

namespace WPFSchedule.ViewModels
{
    public class DataGridViewModel : INotifyPropertyChanged
    {
        private const int DAY_IN_WEEK = 7;

        private DateTime _selectedDay;
        private SqlQuery _sqlQuery;
        private ObservableCollection<ScheduledEvent> _scheduledEvents;

        private RelayCommand _nextWeek;
        private RelayCommand _previousWeek;
        private RelayCommand _switchSelectedDay;
        private RelayCommand _selectCurrentDay;
        private RelayCommand _switchDayToDatePickerDay;
        private RelayCommand _applySelectDayToDatePicker;

        public DataGridViewModel() 
        {
            _sqlQuery = new SqlQuery();
        }

        public DateTime Sunday => SelectedDay.GetStartAndEndWeek().startWeek;
        public DateTime Monday => Sunday.AddDays(1);
        public DateTime Tuesday => Sunday.AddDays(2);
        public DateTime Wednesday => Sunday.AddDays(3);
        public DateTime Thursday => Sunday.AddDays(4);
        public DateTime Friday => Sunday.AddDays(5);
        public DateTime Saturday => SelectedDay.GetStartAndEndWeek().endWeek;

        public IEnumerable<ScheduledEvent> SundayScheduledEvents => ScheduledEvents
            .Where(scheduledEvent => scheduledEvent.EventStart.DayOfWeek == DayOfWeek.Sunday)
            .Select(scheduledEvent => scheduledEvent);

        public IEnumerable<ScheduledEvent> MondayScheduledEvents => ScheduledEvents
            .Where(scheduledEvent => scheduledEvent.EventStart.DayOfWeek == DayOfWeek.Monday)
            .Select(scheduledEvent => scheduledEvent);

        public IEnumerable<ScheduledEvent> TuesdayScheduledEvents => ScheduledEvents
            .Where(scheduledEvent => scheduledEvent.EventStart.DayOfWeek == DayOfWeek.Tuesday)
            .Select(scheduledEvent => scheduledEvent);

        public IEnumerable<ScheduledEvent> WednesdayScheduledEvents => ScheduledEvents
            .Where(scheduledEvent => scheduledEvent.EventStart.DayOfWeek == DayOfWeek.Wednesday)
            .Select(scheduledEvent => scheduledEvent);

        public IEnumerable<ScheduledEvent> ThursdayScheduledEvents => ScheduledEvents
            .Where(scheduledEvent => scheduledEvent.EventStart.DayOfWeek == DayOfWeek.Thursday)
            .Select(scheduledEvent => scheduledEvent);

        public IEnumerable<ScheduledEvent> FridayScheduledEvents => ScheduledEvents
            .Where(scheduledEvent => scheduledEvent.EventStart.DayOfWeek == DayOfWeek.Friday)
            .Select(scheduledEvent => scheduledEvent);

        public IEnumerable<ScheduledEvent> SaturdayScheduledEvents => ScheduledEvents
            .Where(scheduledEvent => scheduledEvent.EventStart.DayOfWeek == DayOfWeek.Saturday)
            .Select(scheduledEvent => scheduledEvent);

        public RelayCommand NextWeek 
        {
            get 
            {
                return _nextWeek ?? (_nextWeek = new RelayCommand(datePicker =>
                {
                    SelectedDay = SelectedDay.AddDays(DAY_IN_WEEK);

                    ApplySelectDayToDatePicker.Execute(datePicker);

                    OnChangWeek();
                }));
            }
        }

        public RelayCommand PreviousWeek 
        {
            get 
            {
                return _previousWeek ?? (_previousWeek = new RelayCommand(datePicker =>
                {
                    SelectedDay = SelectedDay.AddDays(-DAY_IN_WEEK);

                    ApplySelectDayToDatePicker.Execute(datePicker);

                    OnChangWeek();
                }));
            }
        }

        public RelayCommand SwitchSelectedDay 
        {
            get 
            {
                return _switchSelectedDay ?? (_switchSelectedDay = new RelayCommand(date =>
                {
                    TextBox text = (TextBox)date;

                    if (DateTime.TryParse(text.Text, out _))
                    {
                        SelectedDay = DateTime.Parse(text.Text);

                        OnChangWeek();
                    }
                }));
            }
        }

        public RelayCommand SelectCurrentDay 
        {
            get
            {
                return _selectCurrentDay ?? (_selectCurrentDay = new RelayCommand(parameter =>
                {
                    SelectedDay = DateTime.Now;

                    OnChangWeek();
                }));
            }
        }

        public RelayCommand SwitchDayToDatePickerDay 
        {
            get
            {
                return _switchDayToDatePickerDay ?? (_switchDayToDatePickerDay = new RelayCommand(datePicker =>
                {
                    DatePicker picker = (DatePicker)datePicker;

                    SelectedDay = picker.SelectedDate.Value;

                    OnChangWeek();
                }));
            }
        }

        public RelayCommand ApplySelectDayToDatePicker
        {
            get
            {
                return _applySelectDayToDatePicker ?? (_applySelectDayToDatePicker = new RelayCommand(datePicker =>
                {
                    DatePicker picker = (DatePicker)datePicker;

                    picker.SelectedDate = SelectedDay;
                }));
            }
        }

        public DateTime SelectedDay 
        {
            get 
            {
                return _selectedDay == default ? DateTime.Now : _selectedDay;
            }
            set 
            {
                _selectedDay = value;

                OnPropertyChanged("SelectedDay");
                OnSelectedDayChanged();
            }
        }

        public ObservableCollection<ScheduledEvent> ScheduledEvents 
        {
            get 
            {
                return _scheduledEvents ?? (_scheduledEvents = _sqlQuery.SelectScheduledEvents(SelectedDay));
            }
            set 
            {
                _scheduledEvents = value;

                OnPropertyChanged("ScheduledEvents");
            }
        }

        private void OnSelectedDayChanged() 
        {
            ScheduledEvents = _sqlQuery.SelectScheduledEvents(SelectedDay);
        }

        private void OnChangWeek() 
        {
            OnPropertyChanged("Sunday");
            OnPropertyChanged("Monday");
            OnPropertyChanged("Tuesday");
            OnPropertyChanged("Wednesday");
            OnPropertyChanged("Thursday");
            OnPropertyChanged("Friday");
            OnPropertyChanged("Saturday");

            OnPropertyChanged("SundayScheduledEvents");
            OnPropertyChanged("MondayScheduledEvents");
            OnPropertyChanged("TuesdayScheduledEvents");
            OnPropertyChanged("WednesdayScheduledEvents");
            OnPropertyChanged("ThursdayScheduledEvents");
            OnPropertyChanged("FridayScheduledEvents");
            OnPropertyChanged("SaturdayScheduledEvents");
        }

        #region ===================== Notifier ==========================
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string property = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
