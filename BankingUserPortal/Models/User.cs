using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BankingUserPortal.Models;

namespace BankingUserPortal.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
