using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IUintOfWork uintOfWork;

		//private readonly IBaseRepository<Book> repository;

		public BooksController(/*IBaseRepository<Book> repository*/ IUintOfWork uintOfWork)
		{
			this.uintOfWork = uintOfWork;
			//this.repository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//var books = await repository.GetAll();

			var books = await uintOfWork.Books.GetAll();

			return Ok(books);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetById([FromRoute]int id)
		{
			//var book =await repository.GetById(id);

			var book = await uintOfWork.Books.GetById(id);

			return Ok(book);
		}

		//[HttpGet("GetByName")]
		//[Route("{name}")]
		//public async Task<IActionResult> GetByName([FromRoute]string name)
		//{
		//	return Ok(repository.FindAllAsync(b => b.Title == name, new[] { "Author" }));
		//}

		[HttpGet("GetAllWithAuthors")]
		public async Task<IActionResult> GetAllWithAuthors()
		{
			//return Ok(repository.FindAllAsync(b => b.Title.Contains("New Book"), new[] { "Author" }));

			return Ok(await uintOfWork.Books.FindAllAsync(b => b.Title.Contains("New Book"), new[] { "Author" }));
		}

		[HttpGet("GetOrdered")]
		public async Task <IActionResult> GetOrdered()
		{
			//return Ok(await repository.FindAllAsync(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));

			return Ok(await uintOfWork.Books.FindAllAsync(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));
		}

		[HttpPost]
		public IActionResult Add()
		{
			//return Ok(repository.Add(new Book { Title = "New Book", AuthorId = 1 }));
			var book = uintOfWork.Books.Add(new Book { Title = "New Book", AuthorId = 1 });
			uintOfWork.Complete();
			return Ok(book);
		}
	}
}
