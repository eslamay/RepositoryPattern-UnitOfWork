using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly IUintOfWork uintOfWork;

		//private readonly IBaseRepository<Author> repository;

		public AuthorsController(/*IBaseRepository<Author> repository*/ IUintOfWork uintOfWork)
		{
			this.uintOfWork = uintOfWork;
			//this.repository = repository;
		}

		[HttpGet]
		public async Task <IActionResult> GetAll()
		{
			//var authors = await repository.GetAll();

			var authors = await uintOfWork.Authors.GetAll();

			return Ok(authors);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task <IActionResult> GetById([FromRoute]int id)
		{
			//var author =await repository.GetById(id);

			var author = await uintOfWork.Authors.GetById(id);

			return Ok(author);
		}
	}
}
