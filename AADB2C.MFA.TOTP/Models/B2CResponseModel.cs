using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace AADB2C.MFA.TOTP.Models
{
    public class B2CResponseModel
    {
        public string version { get; set; }
        public int status { get; set; }
        public string userMessage { get; set; }


        // Optional claims
        public string qrCodeBitmap { get; set; }
        public string secretKey { get; set; }
        public string timeStepMatched { get; set; }
        public string TOTPUrlResponse { get; set; }

        public B2CResponseModel(string message, HttpStatusCode status)
        {
            this.userMessage = message;
            this.status = (int)status;
            this.version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
