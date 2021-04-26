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

        public ObservableCollection<EventOccurence> SelectedEventOccurence(DateTime selectedDay) 
        {
            ObservableCollection<EventOccurence> eventOccurences = new ObservableCollection<EventOccurence>();



            return eventOccurences;
        }

        private MySqlCommand GetCommandToSelectScheduledEvents(DateTime selectedDay, MySqlConnection connection) 
        {
            (DateTime startWeek, DateTime endWeek) = selectedDay.GetStartAndEndWeek();

            string command = "SELECT  id,\n" +
                    "(SELECT id FROM soft.event_occurence WHERE id = soft.scheduled_event.event_occurence_id) AS event_occurence_id,\n" +
                    "student_group_id,\n" +
                    "theme_id,\n" +
                    "mentor_id,\n" +
                    "lesson_id,\n" +
                    "event_start,\n" +
                    "event_finish\n" +
                    "FROM soft.scheduled_event\n" +
                    "WHERE NOT (event_start > @end AND event_finish > @end)\n" +
                    "AND NOT (event_start < @start AND event_finish < @start)";

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
