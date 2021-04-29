using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("student_group")]
    public class StudentGroup
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("course_id")]
        public long CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("finish_date")]
        public DateTime? FinishDate { get; set; }

        public virtual Course Course { get; set; }

        public virtual IList<Lesson> Lesson { get; set; }

        public virtual IList<MentorOfStudentGroup> MentorsOfStudentGroups { get; set; }
        
        public virtual IList<EventOccurrence> EventOccurances { get; set; }
        
        public virtual IList<StudentOfStudentGroup> StudentsOfStudentGroups { get; set; }
        
        public virtual ICollection<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
