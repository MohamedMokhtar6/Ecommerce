﻿namespace E_Commerce.Core.Models
{
    public class AuthModel
    {
        public string Masseage { get; set; }
        public  bool IsAuthenticated { get; set; }
        public  string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpriesOn { get; set; } 
    }
}
