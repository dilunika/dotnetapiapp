using System.Net;
using System.Threading.Tasks;
using dotnetapiapp.Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace dotnetapiapp.Controllers
{
    [Route("api/accounts/{accountId}")]
    public class AccountController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> GetAccount([FromRoute(Name = "accountId")] string accountId)
        {
            if(accountId == null || accountId.Length == 0) {
                Log.Warning("Returning 404 [NotFound]. No account id sent.");
                return new StatusCodeResult((int) HttpStatusCode.NotFound);
            }
            Account account = await FetchAccountDetails(accountId);
            
            Log.Information("Successfully fetched account details for account: {0} ", accountId);

            return Ok(account);
        }

       private Task<Account> FetchAccountDetails(string accountId)
        {
            Log.Information("Fetching account details");
            return Task.FromResult(new Account(accountId));
        }
    }
}
