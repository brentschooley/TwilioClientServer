using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace TwilioClientServer.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client/Token?ClientName=alice
        public ActionResult Token(string clientName = "alice")
        {
            // Create a TwilioCapability object passing in your credentials.
            var capability = new TwilioCapability("[Your Account SID]", "[Your Auth Token]");

            // Specify that this token allows receiving of incoming calls
            capability.AllowClientIncoming(clientName);

            // Replace "AP*****" with your TwiML App Sid
            capability.AllowClientOutgoing("AP********************************");

            // Return the token as text
            return Content(capability.GenerateToken());
        }

        // /Client/CallXamarin
        public ActionResult CallXamarin()
        {
            var response = new TwilioResponse();
            response.Dial(new Client("alice"));
            return new TwiMLResult(response);
        }

        // /Client/InitiateCall?source=5551231234&target=5554561212
        public ActionResult InitiateCall(string source, string target)
        {
            var response = new TwilioResponse();

            // Add a <Dial> tag that specifies the callerId attribute
            response.Dial(target, new { callerId = source });

            return new TwiMLResult(response);
        }
    }
}