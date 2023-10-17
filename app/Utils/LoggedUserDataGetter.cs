using System.Security.Claims;

namespace app.Utils
{
    public static class LoggedUserDataGetter
    {
        public static string GetRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return "";
            }

            Claim? roleClaim = principal.Claims.SingleOrDefault(c => c.Type.EndsWith("role", true, null));

            if (roleClaim == null)
            {
                return String.Empty;
            }
            return roleClaim.Value;
        }

        public static string GetLogin(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return "";
            }

            Claim? nameClaim = principal.Claims.SingleOrDefault(c => c.Type.EndsWith("name", true, null));

            if (nameClaim == null)
            {
                return String.Empty;
            }
            return nameClaim.Value;
        }
    }
}
