﻿namespace E_Commerce.Domain.Entities.Identity;

public class UserAddress
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public string AppUserId { get; set; }



}