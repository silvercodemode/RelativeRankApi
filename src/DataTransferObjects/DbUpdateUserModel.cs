using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.DataTransferObjects
{
    public class DbUpdateUserModel
    {
        public string? UserToUpdateCurrentUsername { get; set; }
        public string? UserToUpdateNewUsername { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
