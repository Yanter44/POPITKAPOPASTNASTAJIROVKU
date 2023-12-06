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

        [SwaggerOperation(Summary = "Найти книгу по ISBN", Description = "Метод возвращает книгу по введенному isbn")]
        [HttpGet("books/isbn/{isbn}")]
        public async Task<ActionResult> GetBookPoIsbn([FromRoute] int isbn)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [SwaggerOperation(Summary = "Изменить книгу", Description = "Метод Изменяет книгу ")]
        [HttpPut("api/books/{isbn}/edit")]
        public async Task<ActionResult<Book>> EditBook(int isbn, [FromBody] string EditDescription)
        {
            var book = _context.Books.FirstOrDefault(b => b.ISBN == isbn);
            if(book == null) 
            { 
               return NotFound();
            }
            book.Description = EditDescription;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }
        [SwaggerOperation(Summary = "Удалить книгу", Description = "Метод Изменяет книгу ")]
        [HttpDelete("api/books/{id}")]
        public async Task<ActionResult<Book>> Delete([FromRoute] int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
           var allbooks = _context.Books.OrderBy(r => r.Id).ToList();
            for(int i = 0; i < allbooks.Count; i++) 
            {
                allbooks[i].Id = i + 1;
            }
            await _context.SaveChangesAsync();
            return Ok(book);
        }





    }
}