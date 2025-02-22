using E_Commerce.Model.Hr;
using E_Commerce.Model.Inventory;
using E_Commerce.Model.UserMangement;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Deprtments { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Product>().Property(p => p.UrlImg)
                .HasDefaultValue("https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png");

            modelBuilder.Entity<User>().Property(p => p.UrlImag)
                .HasDefaultValue("https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg");

            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 2)");

            //modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);

            // Seed 6 categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Home Appliances" },
                new Category { Id = 3, Name = "Furniture" },
                new Category { Id = 4, Name = "Books" },
                new Category { Id = 5, Name = "Clothing" },
                new Category { Id = 6, Name = "Toys" }
            );

            // Seed 30 products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Gaming Laptop", Price = 999.99m, Quantity = 50, categoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest Model", Price = 699.99m, Quantity = 100, categoryId = 1 },
                new Product { Id = 3, Name = "Fridge", Description = "Double Door", Price = 799.99m, Quantity = 30, categoryId = 2 },
                new Product { Id = 4, Name = "Microwave", Description = "Digital Microwave", Price = 149.99m, Quantity = 25, categoryId = 2 },
                new Product { Id = 5, Name = "Sofa", Description = "Leather Sofa", Price = 599.99m, Quantity = 10, categoryId = 3 },
                new Product { Id = 6, Name = "Bookshelf", Description = "Wooden Bookshelf", Price = 79.99m, Quantity = 15, categoryId = 3 },
                new Product { Id = 7, Name = "Table Lamp", Description = "Modern Lamp", Price = 29.99m, Quantity = 45, categoryId = 3 },
                new Product { Id = 8, Name = "Chair", Description = "Ergonomic Chair", Price = 99.99m, Quantity = 20, categoryId = 3 },
                new Product { Id = 9, Name = "Novel", Description = "Best Seller", Price = 19.99m, Quantity = 200, categoryId = 4 },
                new Product { Id = 10, Name = "Textbook", Description = "Educational", Price = 59.99m, Quantity = 80, categoryId = 4 },
                new Product { Id = 11, Name = "Shirt", Description = "Cotton Shirt", Price = 29.99m, Quantity = 150, categoryId = 5 },
                new Product { Id = 12, Name = "Jeans", Description = "Denim Jeans", Price = 49.99m, Quantity = 120, categoryId = 5 },
                new Product { Id = 13, Name = "Dress", Description = "Summer Dress", Price = 69.99m, Quantity = 60, categoryId = 5 },
                new Product { Id = 14, Name = "Jacket", Description = "Winter Jacket", Price = 99.99m, Quantity = 50, categoryId = 5 },
                new Product { Id = 15, Name = "Teddy Bear", Description = "Large Size", Price = 19.99m, Quantity = 300, categoryId = 6 },
                new Product { Id = 16, Name = "Action Figure", Description = "Superhero Toy", Price = 29.99m, Quantity = 200, categoryId = 6 },
                new Product { Id = 17, Name = "Puzzle", Description = "1000 Pieces", Price = 14.99m, Quantity = 100, categoryId = 6 },
                new Product { Id = 18, Name = "Board Game", Description = "Family Game", Price = 39.99m, Quantity = 70, categoryId = 6 },
                new Product { Id = 19, Name = "Toy Car", Description = "Remote Control Car", Price = 49.99m, Quantity = 150, categoryId = 6 },
                new Product { Id = 20, Name = "Drone", Description = "Mini Drone", Price = 129.99m, Quantity = 50, categoryId = 6 },
                new Product { Id = 21, Name = "Blender", Description = "High-speed Blender", Price = 89.99m, Quantity = 40, categoryId = 2 },
                new Product { Id = 22, Name = "Oven", Description = "Electric Oven", Price = 299.99m, Quantity = 20, categoryId = 2 },
                new Product { Id = 23, Name = "Monitor", Description = "27-inch 4K Monitor", Price = 399.99m, Quantity = 15, categoryId = 1 },
                new Product { Id = 24, Name = "Keyboard", Description = "Mechanical Keyboard", Price = 79.99m, Quantity = 70, categoryId = 1 },
                new Product { Id = 25, Name = "Mouse", Description = "Wireless Mouse", Price = 29.99m, Quantity = 100, categoryId = 1 },
                new Product { Id = 26, Name = "Washing Machine", Description = "Front Load", Price = 699.99m, Quantity = 10, categoryId = 2 },
                new Product { Id = 27, Name = "Dryer", Description = "Electric Dryer", Price = 599.99m, Quantity = 8, categoryId = 2 },
                new Product { Id = 28, Name = "Camera", Description = "Digital Camera", Price = 499.99m, Quantity = 25, categoryId = 1 },
                new Product { Id = 29, Name = "Headphones", Description = "Noise Cancelling", Price = 199.99m, Quantity = 60, categoryId = 1 },
                new Product { Id = 30, Name = "Tablet", Description = "10-inch Tablet", Price = 299.99m, Quantity = 50, categoryId = 1 }
            );

            // Seed 5 departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT"},
                new Department { Id = 3, Name = "Sales"},
                new Department { Id = 4, Name = "Marketing"},
                new Department { Id = 5, Name = "Finance", ManagerId = 5 }
            );


            // Seed 30 employees 
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FName = "Ahmed", LName = "Ali"},
                new Employee { Id = 2, FName = "Mohammed", LName = "Saeed"},
                new Employee { Id = 3, FName = "Youssef", LName = "Khaled"},
                new Employee { Id = 4, FName = "Abdullah", LName = "Ahmed"},
                new Employee { Id = 5, FName = "Sami", LName = "Al-Otaibi"},
                new Employee { Id = 6, FName = "Khaled", LName = "Mahmoud"},
                new Employee { Id = 7, FName = "Omar", LName = "Al-Sheikh"},
                new Employee { Id = 8, FName = "Maher", LName = "Youssef"},
                new Employee { Id = 9, FName = "Hussein", LName = "Mostafa"},
                new Employee { Id = 10, FName = "Ali", LName = "Hassan"},
                new Employee { Id = 11, FName = "Tariq", LName = "Suleiman"},
                new Employee { Id = 12, FName = "Jamal", LName = "Zaidan"},
                new Employee { Id = 13, FName = "Hamad", LName = "Nasser"},
                new Employee { Id = 14, FName = "Khaled", LName = "Amin"},
                new Employee { Id = 15, FName = "Adel", LName = "Salem"},
                new Employee { Id = 16, FName = "Faisal", LName = "Fahad"},
                new Employee { Id = 17, FName = "Saleh", LName = "Abdulrahman"},
                new Employee { Id = 18, FName = "Badr", LName = "Hassan"},
                new Employee { Id = 19, FName = "Marwan", LName = "Ali"},
                new Employee { Id = 20, FName = "Yasser", LName = "Mansour"},
                new Employee { Id = 21, FName = "Saad", LName = "Hussein"},
                new Employee { Id = 22, FName = "Ziyad", LName = "Tariq"},
                new Employee { Id = 23, FName = "Shadi", LName = "Sameer"},
                new Employee { Id = 24, FName = "Salim", LName = "Emad"},
                new Employee { Id = 25, FName = "Sami", LName = "Jasim"},
                new Employee { Id = 26, FName = "Muneer", LName = "Badr"},
                new Employee { Id = 27, FName = "Waleed", LName = "Abdulrahim"},
                new Employee { Id = 28, FName = "Hazem", LName = "Salim"},
                new Employee { Id = 29, FName = "Abdulmalik", LName = "Saud"},
                new Employee { Id = 30, FName = "Imad", LName = "Abdullah"}
            );

            // Seed 30 users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FName = "Ahmed", LName = "Ali", Email = "ahmed.ali@example.com", Password = "password1" },
                new User { Id = 2, FName = "Mohammed", LName = "Saeed", Email = "mohammed.saeed@example.com", Password = "password2" },
                new User { Id = 3, FName = "Yousef", LName = "Khaled", Email = "yousef.khaled@example.com", Password = "password3" },
                new User { Id = 4, FName = "Abdullah", LName = "Omar", Email = "abdullah.omar@example.com", Password = "password4" },
                new User { Id = 5, FName = "Omar", LName = "Farouk", Email = "omar.farouk@example.com", Password = "password5" },
                new User { Id = 6, FName = "Ali", LName = "Hassan", Email = "ali.hassan@example.com", Password = "password6" },
                new User { Id = 7, FName = "Hussein", LName = "Mustafa", Email = "hussein.mustafa@example.com", Password = "password7" },
                new User { Id = 8, FName = "Salim", LName = "Ibrahim", Email = "salim.ibrahim@example.com", Password = "password8" },
                new User { Id = 9, FName = "Fahd", LName = "Mansour", Email = "fahd.mansour@example.com", Password = "password9" },
                new User { Id = 10, FName = "Khalil", LName = "Salah", Email = "khalil.salah@example.com", Password = "password10" },
                new User { Id = 11, FName = "Tariq", LName = "Abdullah", Email = "tariq.abdullah@example.com", Password = "password11" },
                new User { Id = 12, FName = "Sami", LName = "Zaid", Email = "sami.zaid@example.com", Password = "password12" },
                new User { Id = 13, FName = "Mahmoud", LName = "Fawaz", Email = "mahmoud.fawaz@example.com", Password = "password13" },
                new User { Id = 14, FName = "Bilal", LName = "Nasser", Email = "bilal.nasser@example.com", Password = "password14" },
                new User { Id = 15, FName = "Mustafa", LName = "Sayed", Email = "mustafa.sayed@example.com", Password = "password15" },
                new User { Id = 16, FName = "Bassam", LName = "Adel", Email = "bassam.adel@example.com", Password = "password16" },
                new User { Id = 17, FName = "Amir", LName = "Jamal", Email = "amir.jamal@example.com", Password = "password17" },
                new User { Id = 18, FName = "Zaid", LName = "Hussein", Email = "zaid.hussein@example.com", Password = "password18" },
                new User { Id = 19, FName = "Nabil", LName = "Othman", Email = "nabil.othman@example.com", Password = "password19" },
                new User { Id = 20, FName = "Rami", LName = "Anwar", Email = "rami.anwar@example.com", Password = "password20" },
                new User { Id = 21, FName = "Jamil", LName = "Qasim", Email = "jamil.qasim@example.com", Password = "password21" },
                new User { Id = 22, FName = "Hadi", LName = "Rashid", Email = "hadi.rashid@example.com", Password = "password22" },
                new User { Id = 23, FName = "Walid", LName = "Hamza", Email = "walid.hamza@example.com", Password = "password23" },
                new User { Id = 24, FName = "Adnan", LName = "Salim", Email = "adnan.salim@example.com", Password = "password24" },
                new User { Id = 25, FName = "Anas", LName = "Younes", Email = "anas.younes@example.com", Password = "password25" },
                new User { Id = 26, FName = "Saif", LName = "Shadi", Email = "saif.shadi@example.com", Password = "password26" },
                new User { Id = 27, FName = "Othman", LName = "Ziyad", Email = "othman.ziyad@example.com", Password = "password27" },
                new User { Id = 28, FName = "Mounir", LName = "Ghassan", Email = "mounir.ghassan@example.com", Password = "password28" },
                new User { Id = 29, FName = "Kareem", LName = "Ishaq", Email = "kareem.ishaq@example.com", Password = "password29" },
                new User { Id = 30, FName = "Salah", LName = "Firas", Email = "salah.firas@example.com", Password = "password30" }
            );
        }
    }
}
