using Microsoft.AspNetCore.Identity;

namespace PowerPath.Web.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
