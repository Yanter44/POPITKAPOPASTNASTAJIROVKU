using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POPITKAPOPASTNASTAJIROVKU.Entyties;
using Swashbuckle.AspNetCore.Annotations;

namespace POPITKAPOPASTNASTAJIROVKU.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
       

        private readonly ILogger<UserController> _logger;
        private readonly Context _context;
        public UserController(ILogger<UserController> logger,  Context context)
        {
            _logger = logger;
            _context = context;
           
        }
        [SwaggerOperation(Summary = "Получить список всех книг", Description = "Этот метод возвращает список всех книг")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetItems()
        {
      
            return await _context.Set<Book>().ToListAsync();
        }
        [SwaggerOperation(Summary = "Добавить вашу книгу", Description = "Этот метод возвращает список всех книг")]
        [HttpPost]
        public async Task<ActionResult<Book>> CreateItem(Book book) 
        {
            _context.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItems), new { id = book.Id }, book);
        }
     
        [SwaggerOperation(Summary = "Найти книгу по айди", Description = "Метод возвращает книгу по введенному айди")]
        [HttpGet("books/{bookId}")]
        public async Task<ActionResult> GetBookPoId(Book book, [FromRoute] int bookId) 
        {
            var books = await _context.FindAsync<Book>(bookId);
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }


    }
}