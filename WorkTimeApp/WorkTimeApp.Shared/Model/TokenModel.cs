using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkTimeApp.Shared.Model
{
    public class TokenModel
    {
        // Ensure the Token property is public to resolve the CS0122 error  
        public string Token { get; set; }
        public int Id { get; set; }
    }
}
