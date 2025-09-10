using System.Security.Claims;

namespace Kyzmat.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            if (user == null) return null;

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId, out var guid))
            {
                return guid;
            }

            return null;
        }
    }
}
