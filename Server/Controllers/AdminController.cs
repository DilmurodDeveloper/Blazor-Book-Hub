using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBookHub.Server.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public AdminController(IBookService bookService, ICategoryService categoryService, IUserService userService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var booksCount = await _bookService.GetCountAsync();
            var categoriesCount = await _categoryService.GetCountAsync();
            var usersCount = await _userService.GetCountAsync();

            return Ok(new
            {
                BooksCount = booksCount,
                CategoriesCount = categoriesCount,
                UsersCount = usersCount
            });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
