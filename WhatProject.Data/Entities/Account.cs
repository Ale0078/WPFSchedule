using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WhatProject.Data.Enums;

namespace WhatProject.Data.Entities
{
    [Table("account")]
    public class Account
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("role")]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; } = UserRole.NotAssigned;

        [MaxLength(30)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(65)]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(65)]
        [Column("salt")]
        public string Salt { get; set; }

        [Required]
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [MaxLength(100)]
        [Column("forgot_password_token")]
        public string ForgotPasswordToken { get; set; }

        [Column("forgot_token_gen_date")]
        public DateTime? ForgotTokenGenDate { get; set; }

        [Column("avatar_id")]
        public long? AvatarId { get; set; }

        public virtual Attachment Avatar { get; set; }

        public virtual ICollection<Mentor> Mentors { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Secretary> Secretaries { get; set; }
    }
}
