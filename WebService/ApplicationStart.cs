using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShipOfPassage.WebService
{   
    /// <summary>
    /// Added for Register.
    /// </summary>
    public class ApplicationStart
    {
        protected void Application_Start()
        {
            // Pass a delegate to the Configure method.
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}