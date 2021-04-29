using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("course")]
    public class Course
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; }

        public virtual ICollection<MentorOfCourse> MentorsOfCourses { get; set; }
        
        public virtual ICollection<StudentGroup> StudentGroup { get; set; }
    }
}
