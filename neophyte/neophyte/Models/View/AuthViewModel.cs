using System;

namespace neophyte.Models.View
{
    public class AuthViewModel
    {
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}