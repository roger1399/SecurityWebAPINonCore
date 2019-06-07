using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebapiNonCore.Filters
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response =  actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else

            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedauthtoken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] usernamepass = decodedauthtoken.Split(':');
                string username = usernamepass[0];
                string pass = usernamepass[1];

                if (username == "roger" && pass == "apiit")
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }
            }

        }



        //public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        //{
        //    if (actionContext.Request.Headers.Authorization == null)
        //    {
        //        return Task.FromResult<HttpResponseMessage>(actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized));
        //    }
        //    else

        //    {
        //        string authToken = actionContext.Request.Headers.Authorization.Parameter;
        //        string decodedauthtoken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
        //        string[] usernamepass = decodedauthtoken.Split(':');
        //        string username = usernamepass[0];
        //        string pass = usernamepass[1];

        //        if (username == "roger" && pass == "apiit")
        //        {
        //            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
        //        }
        //        else
        //        {
        //            return Task.FromResult<HttpResponseMessage>(actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized));
        //        }
        //    }

        //    return continuation();


        //}


        //private async Task<HttpResponseMessage> getAuthorizationResponse(HttpActionContext actionContext)
        //{
        //    if (actionContext.Request.Headers.Authorization == null)
        //    {
        //        return   actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
        //    }

        //    return actionContext.Request.CreateResponse(System.Net.HttpStatusCode.);
        //}
    }
}