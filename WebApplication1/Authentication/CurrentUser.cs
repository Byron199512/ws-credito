using System.Security.Claims;

namespace WebApplication1.Authentication
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetIdUser()
        {
            var user = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            string id = user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return id;
        }
    }
}
