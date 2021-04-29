using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("attachment_of_homework_student")]
    public class AttachmentOfHomeworkStudent
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("homework_student_id")]
        public long HomeworkStudentId { get; set; }

        [Required]
        [Column("attachment_id")]
        public long AttachmentId { get; set; }

        public HomeworkStudent HomeworkStudent { get; set; }

        public Attachment Attachment { get; set; }
    }
}
