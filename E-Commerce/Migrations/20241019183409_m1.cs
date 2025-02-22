using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Inventory");

            migrationBuilder.EnsureSchema(
                name: "Hr");

            migrationBuilder.EnsureSchema(
                name: "UserMangement");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UserMangement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UrlImag = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_categoryId",
                        column: x => x.categoryId,
                        principalSchema: "Inventory",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Hr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Hr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Hr",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Home Appliances" },
                    { 3, "Furniture" },
                    { 4, "Books" },
                    { 5, "Clothing" },
                    { 6, "Toys" }
                });

            migrationBuilder.InsertData(
                schema: "Hr",
                table: "Departments",
                columns: new[] { "Id", "ManagerId", "Name" },
                values: new object[,]
                {
                    { 1, null, "HR" },
                    { 2, null, "IT" },
                    { 3, null, "Sales" },
                    { 4, null, "Marketing" }
                });

            migrationBuilder.InsertData(
                schema: "Hr",
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "FName", "LName" },
                values: new object[,]
                {
                    { 1, null, "Ahmed", "Ali" },
                    { 2, null, "Mohammed", "Saeed" },
                    { 3, null, "Youssef", "Khaled" },
                    { 4, null, "Abdullah", "Ahmed" },
                    { 5, null, "Sami", "Al-Otaibi" },
                    { 6, null, "Khaled", "Mahmoud" },
                    { 7, null, "Omar", "Al-Sheikh" },
                    { 8, null, "Maher", "Youssef" },
                    { 9, null, "Hussein", "Mostafa" },
                    { 10, null, "Ali", "Hassan" },
                    { 11, null, "Tariq", "Suleiman" },
                    { 12, null, "Jamal", "Zaidan" },
                    { 13, null, "Hamad", "Nasser" },
                    { 14, null, "Khaled", "Amin" },
                    { 15, null, "Adel", "Salem" },
                    { 16, null, "Faisal", "Fahad" },
                    { 17, null, "Saleh", "Abdulrahman" },
                    { 18, null, "Badr", "Hassan" },
                    { 19, null, "Marwan", "Ali" },
                    { 20, null, "Yasser", "Mansour" },
                    { 21, null, "Saad", "Hussein" },
                    { 22, null, "Ziyad", "Tariq" },
                    { 23, null, "Shadi", "Sameer" },
                    { 24, null, "Salim", "Emad" },
                    { 25, null, "Sami", "Jasim" },
                    { 26, null, "Muneer", "Badr" },
                    { 27, null, "Waleed", "Abdulrahim" },
                    { 28, null, "Hazem", "Salim" },
                    { 29, null, "Abdulmalik", "Saud" },
                    { 30, null, "Imad", "Abdullah" }
                });

            migrationBuilder.InsertData(
                schema: "UserMangement",
                table: "Users",
                columns: new[] { "Id", "Email", "FName", "LName", "Password", "UrlImag" },
                values: new object[,]
                {
                    { 1, "ahmed.ali@example.com", "Ahmed", "Ali", "password1", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 2, "mohammed.saeed@example.com", "Mohammed", "Saeed", "password2", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 3, "yousef.khaled@example.com", "Yousef", "Khaled", "password3", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 4, "abdullah.omar@example.com", "Abdullah", "Omar", "password4", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 5, "omar.farouk@example.com", "Omar", "Farouk", "password5", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 6, "ali.hassan@example.com", "Ali", "Hassan", "password6", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 7, "hussein.mustafa@example.com", "Hussein", "Mustafa", "password7", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 8, "salim.ibrahim@example.com", "Salim", "Ibrahim", "password8", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 9, "fahd.mansour@example.com", "Fahd", "Mansour", "password9", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 10, "khalil.salah@example.com", "Khalil", "Salah", "password10", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 11, "tariq.abdullah@example.com", "Tariq", "Abdullah", "password11", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 12, "sami.zaid@example.com", "Sami", "Zaid", "password12", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 13, "mahmoud.fawaz@example.com", "Mahmoud", "Fawaz", "password13", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 14, "bilal.nasser@example.com", "Bilal", "Nasser", "password14", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 15, "mustafa.sayed@example.com", "Mustafa", "Sayed", "password15", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 16, "bassam.adel@example.com", "Bassam", "Adel", "password16", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 17, "amir.jamal@example.com", "Amir", "Jamal", "password17", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 18, "zaid.hussein@example.com", "Zaid", "Hussein", "password18", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 19, "nabil.othman@example.com", "Nabil", "Othman", "password19", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 20, "rami.anwar@example.com", "Rami", "Anwar", "password20", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 21, "jamil.qasim@example.com", "Jamil", "Qasim", "password21", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 22, "hadi.rashid@example.com", "Hadi", "Rashid", "password22", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 23, "walid.hamza@example.com", "Walid", "Hamza", "password23", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 24, "adnan.salim@example.com", "Adnan", "Salim", "password24", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 25, "anas.younes@example.com", "Anas", "Younes", "password25", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 26, "saif.shadi@example.com", "Saif", "Shadi", "password26", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 27, "othman.ziyad@example.com", "Othman", "Ziyad", "password27", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 28, "mounir.ghassan@example.com", "Mounir", "Ghassan", "password28", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 29, "kareem.ishaq@example.com", "Kareem", "Ishaq", "password29", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" },
                    { 30, "salah.firas@example.com", "Salah", "Firas", "password30", "https://t3.ftcdn.net/jpg/03/53/11/00/360_F_353110097_nbpmfn9iHlxef4EDIhXB1tdTD0lcWhG9.jpg" }
                });

            migrationBuilder.InsertData(
                schema: "Hr",
                table: "Departments",
                columns: new[] { "Id", "ManagerId", "Name" },
                values: new object[] { 5, 5, "Finance" });

            migrationBuilder.InsertData(
                schema: "Inventory",
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Quantity", "UrlImg", "categoryId" },
                values: new object[,]
                {
                    { 1, "Gaming Laptop", "Laptop", 999.99m, 50, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 },
                    { 2, "Latest Model", "Smartphone", 699.99m, 100, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 },
                    { 3, "Double Door", "Fridge", 799.99m, 30, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 2 },
                    { 4, "Digital Microwave", "Microwave", 149.99m, 25, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 2 },
                    { 5, "Leather Sofa", "Sofa", 599.99m, 10, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 3 },
                    { 6, "Wooden Bookshelf", "Bookshelf", 79.99m, 15, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 3 },
                    { 7, "Modern Lamp", "Table Lamp", 29.99m, 45, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 3 },
                    { 8, "Ergonomic Chair", "Chair", 99.99m, 20, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 3 },
                    { 9, "Best Seller", "Novel", 19.99m, 200, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 4 },
                    { 10, "Educational", "Textbook", 59.99m, 80, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 4 },
                    { 11, "Cotton Shirt", "Shirt", 29.99m, 150, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 5 },
                    { 12, "Denim Jeans", "Jeans", 49.99m, 120, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 5 },
                    { 13, "Summer Dress", "Dress", 69.99m, 60, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 5 },
                    { 14, "Winter Jacket", "Jacket", 99.99m, 50, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 5 },
                    { 15, "Large Size", "Teddy Bear", 19.99m, 300, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 6 },
                    { 16, "Superhero Toy", "Action Figure", 29.99m, 200, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 6 },
                    { 17, "1000 Pieces", "Puzzle", 14.99m, 100, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 6 },
                    { 18, "Family Game", "Board Game", 39.99m, 70, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 6 },
                    { 19, "Remote Control Car", "Toy Car", 49.99m, 150, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 6 },
                    { 20, "Mini Drone", "Drone", 129.99m, 50, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 6 },
                    { 21, "High-speed Blender", "Blender", 89.99m, 40, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 2 },
                    { 22, "Electric Oven", "Oven", 299.99m, 20, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 2 },
                    { 23, "27-inch 4K Monitor", "Monitor", 399.99m, 15, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 },
                    { 24, "Mechanical Keyboard", "Keyboard", 79.99m, 70, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 },
                    { 25, "Wireless Mouse", "Mouse", 29.99m, 100, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 },
                    { 26, "Front Load", "Washing Machine", 699.99m, 10, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 2 },
                    { 27, "Electric Dryer", "Dryer", 599.99m, 8, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 2 },
                    { 28, "Digital Camera", "Camera", 499.99m, 25, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 },
                    { 29, "Noise Cancelling", "Headphones", 199.99m, 60, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 },
                    { 30, "10-inch Tablet", "Tablet", 299.99m, 50, "https://www.gsascheduleservices.com/wp-content/uploads/2021/01/Empty-Box-min-300x300.png", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                schema: "Hr",
                table: "Departments",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                schema: "Hr",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                schema: "Inventory",
                table: "Products",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_ManagerId",
                schema: "Hr",
                table: "Departments",
                column: "ManagerId",
                principalSchema: "Hr",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_ManagerId",
                schema: "Hr",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "UserMangement");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Hr");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Hr");
        }
    }
}
