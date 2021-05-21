using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using WPFSchedule.Models;
using WPFSchedule.Models.Commands;

using WhatProject.Data.Enums;

namespace WPFSchedule.ViewModels
{
    class AdditionalCreationOptionViewModel : INotifyPropertyChanged
    {
        public AdditionalCreationOptionViewModel()
        {
            IntervalList = new List<string>()
            {
                "Day",
                "Week",
                "Month"
            };
        }
        private RelayCommand _saveData;

        private RelayCommand _changeSunday;
        private RelayCommand _changeMonday;
        private RelayCommand _changeTuesday;
        private RelayCommand _changeWednesday;
        private RelayCommand _changeThursday;
        private RelayCommand _changeFriday;
        private RelayCommand _changeSaturday;

        private int _occurrenceTime;

        public List<string> IntervalList { get; }

        public string DisplayedEventOccurrence
        {
            get => IntervalList[1];
        }

        public int OccurrenceTime
        {
            get => _occurrenceTime;
            set
            {
                _occurrenceTime = value;
            }
        }

        public RelayCommand SaveData
        {
            get
            {
                return _saveData ?? (_saveData = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    if (storage.DaysOfWeek.Count == 0)
                    {
                        MessageBox.Show("Error: You should choose any of days", "Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (_occurrenceTime == 2)
                    {
                        storage.Type = (PatternType)4;
                    }
                    else
                    {
                        storage.Type = (PatternType)_occurrenceTime;
                    }

                    storage.Interval = Interval;

                    MessageBox.Show("Success: Your data were saved", "Schedule", MessageBoxButton.OK, MessageBoxImage.Information);
                }));
            }
        }

        public RelayCommand ChangeSunday
        {
            get
            {
                return _changeSunday ?? (_changeSunday = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    SolidColorBrush brush = null;

                    storage.DaysOfWeek = ManageButton(storage.DaysOfWeek, DayOfWeek.Sunday, ref brush);

                    SundayColor = brush;

                    OnPropertyChanged(nameof(SundayColor));
                }));
            }
        }

        public RelayCommand ChangeMonday
        {
            get
            {
                return _changeMonday ?? (_changeMonday = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    SolidColorBrush brush = null;

                    storage.DaysOfWeek = ManageButton(storage.DaysOfWeek, DayOfWeek.Monday, ref brush);

                    MondayColor = brush;

                    OnPropertyChanged(nameof(MondayColor));
                }));
            }
        }

        public RelayCommand ChangeTuesday
        {
            get
            {
                return _changeTuesday ?? (_changeTuesday = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    SolidColorBrush brush = null;

                    storage.DaysOfWeek = ManageButton(storage.DaysOfWeek, DayOfWeek.Tuesday, ref brush);

                    TuesdayColor = brush;

                    OnPropertyChanged(nameof(TuesdayColor));
                }));
            }
        }

        public RelayCommand ChangeWednesday
        {
            get
            {
                return _changeWednesday ?? (_changeWednesday = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    SolidColorBrush brush = null;

                    storage.DaysOfWeek = ManageButton(storage.DaysOfWeek, DayOfWeek.Wednesday, ref brush);

                    WednesdayColor = brush;

                    OnPropertyChanged(nameof(WednesdayColor));
                }));
            }
        }

        public RelayCommand ChangeThursday
        {
            get
            {
                return _changeThursday ?? (_changeThursday = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    SolidColorBrush brush = null;

                    storage.DaysOfWeek = ManageButton(storage.DaysOfWeek, DayOfWeek.Thursday, ref brush);

                    ThursdayColor = brush;

                    OnPropertyChanged(nameof(ThursdayColor));
                }));
            }
        }

        public RelayCommand ChangeFriday
        {
            get
            {
                return _changeFriday ?? (_changeFriday = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    SolidColorBrush brush = null;

                    storage.DaysOfWeek = ManageButton(storage.DaysOfWeek, DayOfWeek.Friday, ref brush);

                    FridayColor = brush;

                    OnPropertyChanged(nameof(FridayColor));
                }));
            }
        }

        public RelayCommand ChangeSaturday
        {
            get
            {
                return _changeSaturday ?? (_changeSaturday = new RelayCommand(storageParameter =>
                {
                    StoragePattern storage = storageParameter as StoragePattern;

                    SolidColorBrush brush = null;

                    storage.DaysOfWeek = ManageButton(storage.DaysOfWeek, DayOfWeek.Saturday, ref brush);

                    SaturdayColor = brush;

                    OnPropertyChanged(nameof(SaturdayColor));
                }));
            }
        }

        public IList<DayOfWeek> ManageButton(IList<DayOfWeek> daysList, DayOfWeek day, ref SolidColorBrush brush)
        {
            if (daysList == null)
            {
                daysList = new List<DayOfWeek>();
            }

            if (!daysList.Contains(day))
            {
                brush = new SolidColorBrush(Colors.DodgerBlue);
                daysList.Add(day);
            }
            else
            {
                brush = new SolidColorBrush(Colors.Gainsboro);
                daysList.Remove(day);
            }

            return daysList;
        }
    
        public int Interval { get; set; } = 1;

        public SolidColorBrush SundayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro);
        public SolidColorBrush MondayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro);
        public SolidColorBrush TuesdayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro);
        public SolidColorBrush WednesdayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro);
        public SolidColorBrush ThursdayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro);
        public SolidColorBrush FridayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro);
        public SolidColorBrush SaturdayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro);

        public DateTime DefaultEndTime { get; } = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month,DateTime.Today.Day);

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
