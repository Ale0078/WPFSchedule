using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities 
{ 
    [Table("homework_from_student")]
    public class HomeworkStudent
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("student_id")]
        public long StudentId { get; set; }

        [Required]
        [Column("homework_id")]
        public long HomeworkId { get; set; }

        [Column("homework_text")]
        public string HomeworkText { get; set; }

        public Student Student { get; set; }

        public Homework Homework { get; set; }

        public ICollection<AttachmentOfHomeworkStudent> AttachmentOfHomeworkStudents { get; set; }
    }
}
