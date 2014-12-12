using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Segment.Model;

namespace Demo1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // we initialize the analytics plugin here
            // NOTE: in real life, this should be read from a configuration file :-)
            Segment.Analytics.Initialize("bsoac9TGYUnIYcEMdyFSvYUfeRzh9dbg");
        }

        private void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs redirectingToIdentityProviderEventArgs)
        {
            var msg = redirectingToIdentityProviderEventArgs.SignInRequestMessage;
            var password = String.Format("{0}.{1}.{2}",
                msg.Realm,
                msg.CurrentTime,
                "application key here");

            var hash = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password));

            redirectingToIdentityProviderEventArgs.SignInRequestMessage.Parameters.Add("challenge", Convert.ToBase64String(hash));
            // redirectingToIdentityProviderEventArgs.SignInRequestMessage.Parameters.Add("minimumSecurityLevel", "10");
        }

        void WSFederationAuthenticationModule_SignedIn(object sender, EventArgs e)
        {
            //Anything that's needed right after succesful session and before hitting the application code goes here
            System.Diagnostics.Trace.WriteLine("Handling SignIn event");

            // we can monitor events using: Segment.Analytics.Client.Track(userId, eventName, options);
            // https://segment.com/docs/libraries/net/
            // User.Identity.Name

            // get the user's email
            var user = ((ClaimsPrincipal) User);

            Segment.Analytics.Client.Identify(User.Identity.GetUserEmail(), new Traits()
            {
                { "Name", user.FindFirst(ClaimTypes.Name).Value },
                { "Email", user.FindFirst(ClaimTypes.Email).Value },
                { "Poljubni podatek", DateTime.UtcNow.ToShortDateString()}
            });

            // register the login event with analytics
            Segment.Analytics.Client.Track(User.Identity.GetUserEmail(), "Login");
            
        }
    }

    /* {D2DBECD9-182D-4BF7-BBE6-342D03CD5DEE} */
    
}
