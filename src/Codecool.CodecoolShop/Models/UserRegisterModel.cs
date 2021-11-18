﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.ViewModels
{
    public class UserRegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare ("Password",
            ErrorMessage = "Password and confirmation password do not match")]
        public string ConfrimPassword { get; set; }
    }
}
