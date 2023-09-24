using Microsoft.AspNetCore.Identity;
using WebApplication2.Model;

namespace WebApplication2.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration1;
        private readonly IHttpContextAccessor accessor;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthService(IConfiguration configuration1, IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager)
        {
            this.configuration1 = configuration1;
            this.accessor = accessor;
            this.userManager = userManager;
        }
        public string loginUser()
        {
            string current = userManager.GetUserId(accessor.HttpContext.User);
            return current;
        }

    }
}
