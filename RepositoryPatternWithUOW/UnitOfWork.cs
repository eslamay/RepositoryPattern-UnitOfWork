using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF
{
	public class UnitOfWork : IUintOfWork
	{
		private readonly ApplicationDbContext dbContext;

		public IBaseRepository<Author> Authors { get; private set; }
		public IBaseRepository<Book> Books { get; private set; }

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
			Authors = new BaseRepository<Author>(dbContext);
			Books = new BaseRepository<Book>(dbContext);
		}

		public int Complete()
		{
			return dbContext.SaveChanges();
		}

		public void Dispose()
		{
			 dbContext.Dispose();
		}
	}
}
