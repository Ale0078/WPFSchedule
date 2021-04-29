using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

using WhatProject.Data;
using WhatProject.Data.Entities;

namespace WPFSchedule.Helpers.Queries
{
    public class ScheduledEventQuery
    {
        private ApplicationContext _context;

        public ScheduledEventQuery() 
        {
            _context = new ApplicationContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public ObservableCollection<ScheduledEvent> GetScheduledEvents(DayOfWeek day, DateTime start, DateTime finish) 
        {
            IEnumerable<ScheduledEvent> scheduledEvents = _context.ScheduledEvents
                        .Where(item => item.EventStart >= start && finish >= item.EventFinish)
                        .Select(item => item)
                        .ToList();

            ObservableCollection<ScheduledEvent> scheduleds = new ObservableCollection<ScheduledEvent>();

            foreach (var item in scheduledEvents.Where(item => item.EventStart.DayOfWeek == day).Select(item => item))
            {
                scheduleds.Add(item);
            }

            return scheduleds;
        }
    }
}
