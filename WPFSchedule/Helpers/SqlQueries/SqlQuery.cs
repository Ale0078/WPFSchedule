using System;
using System.Collections.ObjectModel;
using System.Configuration;
using MySql.Data.MySqlClient;

using WPFSchedule.Models;
using WPFSchedule.Helpers.Extensions;

namespace WPFSchedule.Helpers.SqlQueries
{
    public class SqlQuery
    {
        public ObservableCollection<ScheduledEvent> SelectScheduledEvents(DateTime selectedDay) 
        {
            ObservableCollection<ScheduledEvent> scheduledEvents = new ObservableCollection<ScheduledEvent>();

            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)) 
            {
                connection.Open();

                MySqlCommand query = GetCommandToSelectScheduledEvents(selectedDay, connection);

                query.Prepare();

                using (MySqlDataReader reader = query.ExecuteReader()) 
                {
                    object[] parameters = new object[8];

                    while (reader.Read())
                    {
                        reader.GetValues(parameters);

                        scheduledEvents.Add(parameters.GetScheduledEvent());
                    }
                }
            }

            return scheduledEvents;
        }

        //public ObservableCollection<EventOccurence> SelectedEventOccurence(DateTime selectedDay)
        //{
        //    ObservableCollection<EventOccurence> eventOccurences = new ObservableCollection<EventOccurence>();

        //    using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)) 
        //    {
        //        connection.Open();


        //    }

        //    return eventOccurences;
        //}

        private MySqlCommand GetCommandToSelectScheduledEvents(DateTime selectedDay, MySqlConnection connection) 
        {
            (DateTime startWeek, DateTime endWeek) = selectedDay.GetStartAndEndWeek();

            string command = "SELECT * FROM soft.scheduled_event\n" +
                "WHERE event_start >= @start AND @end >= event_finish";

            MySqlCommand query = new MySqlCommand(command, connection);

            MySqlParameter startWeekParametr = new MySqlParameter()
            {
                ParameterName = "@start",
                MySqlDbType = MySqlDbType.DateTime,
                Value = startWeek
            };
            MySqlParameter endWeekParametr = new MySqlParameter()
            {
                ParameterName = "@end",
                MySqlDbType = MySqlDbType.DateTime,
                Value = endWeek
            };

            query.Parameters.Add(startWeekParametr);
            query.Parameters.Add(endWeekParametr);

            return query;
        }
    }
}
