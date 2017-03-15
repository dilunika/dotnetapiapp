using System;
using System.Net;
using System.Threading.Tasks;
using dotnetapiapp.Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;

namespace dotnetapiapp.Controllers
{
    [Route("api/tokens")]
    public class TokenController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> RequestToken([FromBody] Credentials credentials){

            if(CredentialsAreInvalid(credentials)){
                Log.Warning("Returning 400 [BadRequest]. Provided Credentials are null or empty.");
                return new StatusCodeResult((int) HttpStatusCode.BadRequest);
            }

            if(credentials.Username.Equals("invaliduser")) {
                Log.Warning("Returning 401 [Unauthorized]. Invalid user credentials.");
                return new StatusCodeResult((int) HttpStatusCode.Unauthorized);
            }

            Token token = await GenerateToken(credentials);
            LogContext.PushProperty("Resource", "Customer[" + token.CustomerId + "]");
            Log.Information("Successfully Received Token for Customer: {0} ", token.CustomerId);

            return Ok(token);

        }

        private Task<Token> GenerateToken(Credentials credentials)
        {
            
            var sessionID = Guid.NewGuid().ToString();
            var customerId = "BP" + new Random().NextDouble().ToString().Substring(2);

            return Task.FromResult(new Token(sessionID, customerId));
        }

        private bool CredentialsAreInvalid(Credentials credentials)
        {
            return credentials == null ||
                    credentials.Username == null ||
                    credentials.Username.Length == 0 ||
                    credentials.Password == null ||
                    credentials.Password.Length == 0;
        }

    }
}
