using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeApp.Services
{
    public interface IPasswordHasher<TUser> where TUser : class
    {
        string HashPassword(TUser user, string password);

        PasswordVerificationResult VerifyHashedPassword(
            TUser user, string hashedPassword, string providedPassword);
    }
}
