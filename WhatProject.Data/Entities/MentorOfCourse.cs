using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("mentor_of_course")]
    public class MentorOfCourse
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("course_id")]
        public long? CourseId { get; set; }

        [Column("mentor_id")]
        public long? MentorId { get; set; }

        public virtual Course Course { get; set; }
        
        public virtual Mentor Mentor { get; set; }
    }
}
