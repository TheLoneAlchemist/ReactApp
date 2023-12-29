using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp1.Server.Models.Common
{
    public class ApplicationUser:IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string? ProfilePicture { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        [ForeignKey("RefreshToken")]
        [ValidateNever]
        public Guid? RefreshTokenId { get; set; }
        [ValidateNever]
        public RefreshToken RefreshToken { get; set; }
    }
}
