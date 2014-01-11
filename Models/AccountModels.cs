using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace LMSServices.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Παλιός κωδικός")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Νέος κωδικός")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Επιβεβαίωση Κωδικού")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Όνομα Χρήστη")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Ο {0} πρέπει να περιλαμβάνει τουλάχιστον {2} χαρακτήρες.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Κωδικός")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Επιβεβαίωση Κωδικού")]
        [Compare("Password", ErrorMessage = "Τα πεδία Κωδικός και Επιβεβαίωση Κωδικού είναι διαφορετικά!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Όνομα")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Επώνυμο")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία Πρόσληψης")]
        [DataType(DataType.Date)]
        public string EmploymentDate { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Κινητό Τηλέφωνο")]
        [DataType(DataType.Text)]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Τύπος Υπαλλήλου")]
        [DataType(DataType.Text)]
        public string UserRole { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
