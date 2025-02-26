﻿namespace E_Commerce.Domain.Entities.OrderEntities;

public class Address
{
    public Address(){}
    public Address(string fName, string lName, string street, string city, string country)
    {
        FirstName = fName;
        LastName = lName;
        Street = street;
        City = city;
        Country = country;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}
