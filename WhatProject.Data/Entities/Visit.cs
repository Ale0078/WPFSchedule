using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WhatProject.Data.Entities
{
    [Table("visit")]
    public class Visit
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("student_id")]
        public long StudentId { get; set; }

        [Column("lesson_id")]
        public long? LessonId { get; set; }

        [Column("student_mark")]
        public sbyte? StudentMark { get; set; }

        [Required]
        [Column("presence")]
        public bool Presence { get; set; }

        [MaxLength(1024)]
        [Column("comment")]
        public string Comment { get; set; }

        public virtual Lesson Lesson { get; set; }
        
        public virtual Student Student { get; set; }
    }
}
