﻿using System.ComponentModel.DataAnnotations;

namespace hannahM.Models
{
    public class Account
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
