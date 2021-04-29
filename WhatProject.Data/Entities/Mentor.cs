using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WhatProject.Data.Entities
{
    [Table("mentor")]
    public class Mentor
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("account_id")]
        public long? AccountId { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<Lesson> Lesson { get; set; }

        public virtual ICollection<MentorOfCourse> MentorsOfCourses { get; set; }

        public virtual ICollection<MentorOfStudentGroup> MentorsOfStudentGroups { get; set; }

        public virtual ICollection<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
