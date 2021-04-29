using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WhatProject.Data.Entities
{
    [Table("student_of_student_group")]
    public class StudentOfStudentGroup
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("student_group_id")]
        public long? StudentGroupId { get; set; }

        [Column("student_id")]
        public long? StudentId { get; set; }

        public virtual Student Student { get; set; }
        
        public virtual StudentGroup StudentGroup { get; set; }
    }
}
