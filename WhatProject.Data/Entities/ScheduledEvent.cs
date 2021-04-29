using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("scheduled_event")]
    public class ScheduledEvent 
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("event_occurence_id")]
        public long EventOccurrenceId { get; set; }

        [Column("student_group_id")]
        public long? StudentGroupId { get; set; }

        [Column("theme_id")]
        public long? ThemeId { get; set; }

        [Column("mentor_id")]
        public long? MentorId { get; set; }

        [Column("lesson_id")]
        public long? LessonId { get; set; }

        [Required]
        [Column("event_start")]
        public DateTime EventStart { get; set; }

        [Required]
        [Column("event_finish")]
        public DateTime EventFinish { get; set; }

        public StudentGroup StudentGroup { get; set; }

        public Theme Theme { get; set; }

        public EventOccurrence EventOccurrence { get; set; }

        public Mentor Mentor { get; set; }

        public Lesson Lesson { get; set; }
    }
}
