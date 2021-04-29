using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WhatProject.Data.Enums;

namespace WhatProject.Data.Entities
{
    [Table("event_occurence")]
    public class EventOccurrence
    {
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [ForeignKey("StudentGroup")]
        [Column("student_group_id")]
        public long StudentGroupId { get; set; }

        [Required]
        public virtual StudentGroup StudentGroup { get; set; }

        [Required]
        [Column("event_start")]
        public DateTime EventStart { get; set; }

        [Required]
        [Column("event_finish")]
        public DateTime EventFinish { get; set; }

        [Required]
        [EnumDataType(typeof(PatternType))]
        [Column("pattern")]
        public PatternType Pattern { get; set; }

        [Required]
        [Column("storage")]
        public long Storage { get; set; }

        public virtual ICollection<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
