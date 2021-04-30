using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel; 
using System.Runtime.CompilerServices;
using System.Windows;
using System.Configuration;
using MySql.Data.MySqlClient;

using WPFSchedule.Models.Commands;
using WPFSchedule.Models;
using WPFSchedule.Helpers.SqlQueries;
using WPFSchedule.Helpers.Extensions;
using System.Windows.Navigation;
using System.Windows.Controls;
using WPFSchedule.Views;

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
        private RelayCommand _createSchedule;

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
                return _nextWeek ?? (_nextWeek = new RelayCommand(parameter =>
                {
                    SelectedDay = SelectedDay.AddDays(DAY_IN_WEEK);

                    OnChangeWeek();
                }));
            }
        }

        public RelayCommand PreviousWeek
        {
            get
            {
                return _previousWeek ?? (_previousWeek = new RelayCommand(parameter =>
                {
                    SelectedDay = SelectedDay.AddDays(-DAY_IN_WEEK);

                    OnChangeWeek();
                }));
            }
        }

        public RelayCommand CreateSchedule
        {
            get
            {
                return _createSchedule ?? (_createSchedule = new RelayCommand(parameter =>
                {
                    ScheduleCreatorView scheduleCreator = new ScheduleCreatorView
                    {
                        Owner = App.Current.MainWindow
                    };

                    scheduleCreator.ShowDialog();
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

        private void OnChangeWeek() 
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
