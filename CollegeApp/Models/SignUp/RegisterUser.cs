﻿using System.ComponentModel.DataAnnotations;

namespace UserManagment.Models.SignUp
{
    public class RegisterUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
