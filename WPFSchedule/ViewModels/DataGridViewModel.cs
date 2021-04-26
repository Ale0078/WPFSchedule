using System;
using System.Linq;
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

namespace WPFSchedule.ViewModels
{
    public class DataGridViewModel : INotifyPropertyChanged
    {
        private DateTime _selectedDay;
        private SqlQuery _sqlQuery;
        private ObservableCollection<ScheduledEvent> _scheduledEvents;

        public DataGridViewModel() 
        {
            _sqlQuery = new SqlQuery();
        }

        public DateTime StartWeek => SelectedDay.GetStartAndEndWeek().startWeek;
        public DateTime EndWeek => SelectedDay.GetStartAndEndWeek().endWeek;

        

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

        #region ================================== Inctence SqlQuare ==========================
        private RelayCommand showMessage;

        public RelayCommand ShowMessage 
        {
            get 
            {
                return showMessage
                    ?? (showMessage = new RelayCommand(obj =>
                    {
                        MessageBox.Show(GetDBMessage());
                    }));
            }
        }
        
        private string GetDBMessage() 
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var sqlConnection = new MySqlConnection(connection)) 
            {
                sqlConnection.Open();                

                string query = "SELECT * FROM soft.scheduled_event WHERE soft.scheduled_event.event_finish < @date";
                MySqlCommand command = new MySqlCommand(query, sqlConnection);

                MySqlParameter mySqlParameter = new MySqlParameter
                {
                    ParameterName = "@date",
                    MySqlDbType = MySqlDbType.DateTime,
                    Value = new DateTime(2021, 01, 20)
                };
                command.Parameters.Add(mySqlParameter);
                command.Prepare();

                string answer = "";

                using (MySqlDataReader reader = command.ExecuteReader()) 
                {
                    while (reader.Read()) 
                    {
                        object[] objs = new object[8];

                        _ = reader.GetValues(objs);

                        answer += string.Join("|", objs);

                        answer += "\n";
                    }
                }                

                return answer;
            }
        }
        #endregion

        #region ===================== Notifier ==========================
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string property = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
