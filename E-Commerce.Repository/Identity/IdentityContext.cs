using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Repository.Identity.DataSeeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> options)
    : IdentityDbContext<AppUser>(options)
{
}
