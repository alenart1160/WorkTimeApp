using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeApp.Model
{
    public class User
    {
        public long Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? NIP { get; set; }
    }
}
