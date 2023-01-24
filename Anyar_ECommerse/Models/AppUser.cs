using Microsoft.AspNetCore.Identity;

namespace Anyar_ECommerse.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
