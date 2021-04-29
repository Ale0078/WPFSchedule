using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("lesson")]
    public class Lesson
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("mentor_id")]
        public long? MentorId { get; set; }

        [Column("student_group_id")]
        public long? StudentGroupId { get; set; }

        [Column("theme_id")]
        public long? ThemeId { get; set; }

        [Required]
        [Column("lesson_date")]
        public DateTime LessonDate { get; set; }

        public virtual Mentor Mentor { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Theme Theme { get; set; }
        
        public virtual ICollection<Homework> Homeworks { get; set; }

        public virtual IList<Visit> Visits { get; set; }

        [Required]
        public virtual ScheduledEvent ScheduledEvent { get; set; }
    }
}
