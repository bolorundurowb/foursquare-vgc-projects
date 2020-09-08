using System;

namespace api.Models.View
{
    public class AuthViewModel
    {
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public AdminViewModel Admin { get; set; }
    }
}