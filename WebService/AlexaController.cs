using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services;

namespace ShipOfPassage.WebService
{
    /// <summary>
    /// The Alexa Webservice controller.
    /// Use if imlementing async calls at any point.
    /// </summary>
    [WebService(Namespace = "https://shipofpassagegm.azurewebsites.net//", Name = "ShipOfPassage Alexa Entry", Description = "The entry point for Alexa requests.")]
    public class AlexaController : ApiController
    {
        /// <summary>
        /// The Alexa API controller. Used to route all requests to 
        /// my endpoint.
        /// </summary>
        [RequireHttps]
        [System.Web.Http.Route("api/alexa")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage ShipOfPassage()
        {
            var speechlet = new GameSpeechlet();
            Console.Out.WriteLine("Got HttpRequest from Alexa: " + Request.ToString());
            return speechlet.GetResponse(Request);
        }
        
    }
}