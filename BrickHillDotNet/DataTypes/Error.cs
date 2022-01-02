using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH_API_SHIT
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
