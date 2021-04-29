using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Collections.Generic;

namespace WhatProject.Data.Entities
{
    [Table("attachment")]
    public class Attachment 
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        [Column("created_by_account_id")]
        public long CreatedByAccountId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("container_name")]
        public string ContainerName { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("file_name")]
        public string FileName { get; set; }

        [Required]
        public virtual Account Account { get; set; }

        public virtual ICollection<AttachmentOfHomework> AttachmentsOfHomework { get; set; }

        public virtual ICollection<AttachmentOfHomeworkStudent> AttachmentOfHomeworkStudents { get; set; }
    }
}
