﻿namespace API.DTOs.Regestiration
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
