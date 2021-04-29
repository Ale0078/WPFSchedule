using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("mentor_of_student_group")]
    public class MentorOfStudentGroup 
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("mentor_id")]
        public long? MentorId { get; set; }

        [Column("student_group_id")]
        public long? StudentGroupId { get; set; }

        public virtual Mentor Mentor { get; set; }
        
        public virtual StudentGroup StudentGroup { get; set; }
    }
}
