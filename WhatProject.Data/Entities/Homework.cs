using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("homework")]
    public class Homework
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("due_date")]
        public DateTime? DueDate { get; set; }

        [Column("task_text")]
        public string TaskText { get; set; }

        [Required]
        [Column("lesson_id")]
        public long LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public virtual IList<AttachmentOfHomework> AttachmentsOfHomework { get; set; }

        public virtual IList<HomeworkStudent> HomeworkStudents { get; set; }
    }
}
