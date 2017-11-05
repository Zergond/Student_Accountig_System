using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Identity
{
   public class AppUser:IdentityUser
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Last name can't be longer than 20 characters")]
        public string LastName { get; set; }
        [Required]
        [Range(0, 120, ErrorMessage = "Age has to be between 0 and 120")]
        public int Age { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime RegisteredDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StudyDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
