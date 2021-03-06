using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using WPFSchedule.Models.Commands;
using WPFSchedule.Helpers.Extensions;

using WPFSchedule.Helpers.Queries;
using WhatProject.Data.Entities;

using WPFSchedule.Views;
using System.Windows;

namespace WPFSchedule.ViewModels
{
    public class DataGridViewModel : INotifyPropertyChanged
    {
        private const int DAY_IN_WEEK = 7;
        
        private DateTime _selectedDay;
        private ScheduledEventQuery _query;
        private Visibility frameVisibility;

        private RelayCommand _nextWeek;
        private RelayCommand _previousWeek;
        private RelayCommand _selectCurrentDay;
        private RelayCommand _createSchedule;

        public DataGridViewModel()
        {
            _query = new ScheduledEventQuery();
            frameVisibility = Visibility.Collapsed;
        }

        public Visibility FrameVisibility
        {
            get => frameVisibility;
            set 
            {
                frameVisibility = value;
                OnPropertyChanged();
            }
        } 

        public DateTime Sunday => SelectedDay.GetStartAndEndWeek().startWeek;
        public DateTime Monday => Sunday.AddDays(1);
        public DateTime Tuesday => Sunday.AddDays(2);
        public DateTime Wednesday => Sunday.AddDays(3);
        public DateTime Thursday => Sunday.AddDays(4);
        public DateTime Friday => Sunday.AddDays(5);
        public DateTime Saturday => SelectedDay.GetStartAndEndWeek().endWeek;

        public ObservableCollection<ScheduledEvent> SundayScheduledEvents => 
            _query.GetScheduledEvents(DayOfWeek.Sunday, Sunday, Saturday);

        public ObservableCollection<ScheduledEvent> MondayScheduledEvents =>
            _query.GetScheduledEvents(DayOfWeek.Monday, Sunday, Saturday);

        public ObservableCollection<ScheduledEvent> TuesdayScheduledEvents =>
            _query.GetScheduledEvents(DayOfWeek.Tuesday, Sunday, Saturday);

        public ObservableCollection<ScheduledEvent> WednesdayScheduledEvents =>
            _query.GetScheduledEvents(DayOfWeek.Wednesday, Sunday, Saturday);

        public ObservableCollection<ScheduledEvent> ThursdayScheduledEvents =>
            _query.GetScheduledEvents(DayOfWeek.Thursday, Sunday, Saturday);

        public ObservableCollection<ScheduledEvent> FridayScheduledEvents =>
            _query.GetScheduledEvents(DayOfWeek.Friday, Sunday, Saturday);

        public ObservableCollection<ScheduledEvent> SaturdayScheduledEvents =>
            _query.GetScheduledEvents(DayOfWeek.Saturday, Sunday, Saturday);

        public RelayCommand NextWeek
        {
            get
            {
                return _nextWeek ?? (_nextWeek = new RelayCommand(datePicker =>
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
                return _previousWeek ?? (_previousWeek = new RelayCommand(datePicker =>
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
                    FrameVisibility = Visibility.Visible;
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

                    OnChangeWeek();
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
                OnChangeWeek();
            }
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