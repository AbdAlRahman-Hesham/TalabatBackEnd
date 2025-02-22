using E_Commerce.Data;
using E_Commerce.Model.Hr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AppController(AppDbContext db)
        {
            _db = db;
        }

        
        [HttpGet("/Departemts")]
        public async Task<IActionResult> GetDepartemts()
        {
            var Departemts = await _db.Deprtments.ToListAsync();
            if (Departemts != null)
            {
                return Ok(Departemts);
                
            }
            else
            {
                return NotFound("No Departemts Exist");
            }
        }
        [HttpGet("/Employees")]
        public async Task<IActionResult> GetEmployees()
        {
            var Employees = await _db.Employees.ToListAsync();
            if (Employees !=null)
            {
                return Ok(Employees);
                
            }
            else
            {
                return NotFound("No Employees Exist");
            }
        }  
        [HttpGet("/Products")]
        public async Task<IActionResult> GetProducts()
        {
            var Products = await _db.Products.ToListAsync();
            if (Products != null)
            {
                return Ok(Products);
                
            }
            else
            {
                return NotFound("No Products Exist");
            }
        }  
        
        [HttpGet("/Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var Categories = await _db.Categories.ToListAsync();
            if (Categories != null)
            {
                return Ok(Categories);
                
            }
            else
            {
                return NotFound("No Categories Exist");
            }
        }
        [HttpGet("/Users")]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _db.users.ToListAsync();
            if (Users != null)
            {
                return Ok(Users);
                
            }
            else
            {
                return NotFound("No Users Exist");
            }
        }

        [HttpPost("/AddEmployee")]
        public async Task<IActionResult>
            AddEmployee(string FName, string LName, int DeprtmentId)
        {
            if (!string.IsNullOrEmpty(FName) && !string.IsNullOrEmpty(LName) && DeprtmentId > 0)
            {
                Employee newEmp = new Employee()
                {
                    DepartmentId = DeprtmentId,
                    FName = FName,
                    LName = LName
                };

                await _db.Employees.AddAsync(newEmp);
                await _db.SaveChangesAsync(); // SaveChangesAsync for async behavior

                return Ok(newEmp);
            }
            else
            {
                return BadRequest("Invalid input parameters");
            }
        }





    }
}
