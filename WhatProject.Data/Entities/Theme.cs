using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WhatProject.Data.Entities
{
    [Table("theme")]
    public class Theme
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
