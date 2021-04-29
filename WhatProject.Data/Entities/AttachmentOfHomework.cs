using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("attachment_of_homework")]
    public class AttachmentOfHomework
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("homework_id")]
        public long HomeworkId { get; set; }

        [Required]
        [Column("attachment_id")]
        public long AttachmentId { get; set; }

        public virtual Attachment Attachment { get; set; }

        public virtual Homework Homework { get; set; }
    }
}
