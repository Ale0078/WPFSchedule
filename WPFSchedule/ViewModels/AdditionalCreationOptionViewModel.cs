using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFSchedule.Models;
using WPFSchedule.Models.Commands;
using WPFSchedule.Models.Enums;

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

            _storage.DaysOfWeek = new List<DayOfWeek>();

            //_daysCollection = new ObservableCollection<string>();

            //for (int i = 0; i < 7; i++)
            //{
            //    _daysCollection.Add(new SolidColorBrush(Colors.Gainsboro).ToString());
            //}
        }

        private RelayCommand _addWeekDate;
        private RelayCommand _switchEndDate;
        private RelayCommand _saveData;

        //private RelayCommand _changeDateList

        private int _occurrenceTime;
        private readonly bool _defaultEndTime = true;
       // private ObservableCollection<string> _daysCollection;
        private StoragePattern _storage = new StoragePattern();

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
                return _saveData ?? (_saveData = new RelayCommand(parameter =>
                {
                    if (_storage.DaysOfWeek.Count == 0)
                    {
                        MessageBox.Show("Error: You should choose any of days", "Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (_occurrenceTime == 2)
                    {
                        _storage.Type = (PatternType)4;
                    }
                    else
                    {
                        _storage.Type = (PatternType)_occurrenceTime;
                    }

                    _storage.Interval = Interval;

                    MessageBox.Show("Success: Your data were saved", "Schedule", MessageBoxButton.OK, MessageBoxImage.Information);
                }));
            }
        }

        //public RelayCommand SwitchEndDate
        //{
        //    get
        //    {
        //        return _switchEndDate ?? (_switchEndDate = new RelayCommand(parameter =>
        //        {
        //            if (_defaultEndTime)
        //            {

        //            }

        //            //OnPropertyChanged(updateDay);
        //        }));
        //    }
        //}

    //    public RelayCommand ChangeSundayDate
    //    {
    //        get
    //        {
    //            return _addWeekDate ?? (_addWeekDate = new RelayCommand(button =>
    //            {
    //            }
    //}
    //    }

        public RelayCommand ChangeDateList
        {
            get
            {
                return _addWeekDate ?? (_addWeekDate = new RelayCommand(button =>
                {
                    Button dateButton = button as Button;

                    int index = 0;

                    string updateDay = "";

                    switch (dateButton.Name)
                    {
                        case nameof(DayOfWeek.Sunday):
                            index = 0;
                            break;
                        case nameof(DayOfWeek.Monday):
                            index = 1;
                            break;
                        case nameof(DayOfWeek.Tuesday):
                            index = 2;
                            break;
                        case nameof(DayOfWeek.Wednesday):
                            index = 3;
                            break;
                        case nameof(DayOfWeek.Thursday):
                            index = 4;
                            break;
                        case nameof(DayOfWeek.Friday):
                            index = 5;
                            break;
                        case nameof(DayOfWeek.Saturday):
                            index = 6;
                            break;
                    }

                    var background = dateButton.Background as SolidColorBrush;
                    var color = background.Color;

                    if (!_storage.DaysOfWeek.Contains((DayOfWeek)index))
                    {
                        dateButton.Background = new SolidColorBrush(Colors.DodgerBlue);
                        _storage.DaysOfWeek.Add((DayOfWeek)index);
                    }
                    else
                    {
                        dateButton.Background = new SolidColorBrush(Colors.Gainsboro);
                        _storage.DaysOfWeek.Remove((DayOfWeek)index);
                    }


                  //  _daysCollection[index] = color.ToString();

                   // OnPropertyChanged(updateDay);
                }));
            }
        }

        public int Interval { get; set; } = 1;

        //public string SundayColor { get; set; } = new SolidColorBrush(Colors.Gainsboro).ToString();

        //public string SundayColor
        //{
        //    get
        //    {
        //        return _daysCollection[0];
        //    }
        //    set
        //    {
        //        _daysCollection[0] = value;
        //    }
        //}

        //public string MondayColor
        //{
        //    get
        //    {
        //        return _daysCollection[1];
        //    }
        //    set
        //    {
        //        _daysCollection[1] = value;
        //    }
        //}

        //public string TuesdayColor
        //{
        //    get
        //    {
        //        return _daysCollection[2];
        //    }
        //    set
        //    {
        //        _daysCollection[2] = value;
        //    }
        //}

        //public string WednesdayColor
        //{
        //    get
        //    {
        //        return _daysCollection[3];
        //    }
        //    set
        //    {
        //        _daysCollection[3] = value;
        //    }
        //}

        //public string ThursdayColor
        //{
        //    get
        //    {
        //        return _daysCollection[4];
        //    }
        //    set
        //    {
        //        _daysCollection[4] = value;
        //    }
        //}

        //public string FridayColor
        //{
        //    get
        //    {
        //        return _daysCollection[5];
        //    }
        //    set
        //    {
        //        _daysCollection[5] = value;
        //    }
        //}

        //public string SaturdayColor
        //{
        //    get
        //    {
        //        return _daysCollection[6];
        //    }
        //    set
        //    {
        //        _daysCollection[6] = value;
        //    }
        //}

        public DateTime DefaultEndTime { get; } = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month,DateTime.Today.Day);


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
