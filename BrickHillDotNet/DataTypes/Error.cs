using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHillDotNet
{

    // Error error = JsonConvert.DeserializeObject<Error>(myJsonResponse); 
    public class ErrorInfo
    {
        public string message { get; set; }
        public string prettyMessage { get; set; }
    }

    public class Error
    {
        public ErrorInfo error { get; set; }
    }
}
