using System.Security.Claims;
using System.Web.Http;
using System.Linq;


namespace SelfHost.Controllers
{
    [Authorize]
    public class IdentityController: ApiController
    {
        public dynamic Get()
        {
            var principal = User as ClaimsPrincipal;

            return from c in principal.Identities.First().Claims
                   select new
                   {
                       c.Type,
                       c.Value
                   };
        }
    }
}