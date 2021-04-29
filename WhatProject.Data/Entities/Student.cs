using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("student")]
    public class Student 
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("account_id")]
        public long? AccountId { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<StudentOfStudentGroup> StudentsOfStudentGroups { get; set; }
        
        public virtual ICollection<HomeworkStudent> HomeworkStudents { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
