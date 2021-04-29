using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatProject.Data.Entities
{
    [Table("secretary")]
    public class Secretary 
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("account_id")]
        public long? AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
