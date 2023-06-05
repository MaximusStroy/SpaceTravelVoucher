using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceTravelVoucher.Main.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public RoleEnum Role { get; set; }
    }

    public enum RoleEnum
    {
        Admin,
        Manager,
        Member
    }
}
