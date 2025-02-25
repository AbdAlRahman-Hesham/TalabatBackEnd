﻿using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Entities.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }

    public UserAddress Address { get; set; }
}
