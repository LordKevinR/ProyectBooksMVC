using BooksMVC.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BooksMVC.Services
{
	public interface IBookReposiroty
	{
		Task Create(Book book);
		Task Delete(int id);
		Task<IEnumerable<Book>> Get();
		Task<Book> GetById(int id);
		Task Update(Book book);
	}
	public class BookRepository : IBookReposiroty
	{
		private readonly string connectionString;

        public BookRepository(IConfiguration configuration)
        {
			connectionString = configuration.GetConnectionString("DefaultConnection");
        }

		public async Task Create(Book book)
		{
			using var connection = new SqlConnection(connectionString);
			var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Books (Title, Description, PageCount, Excerpt, PublishDate)
													VALUES (@Title, @Description, @PageCount, @Excerpt, @PublishDate);
													SELECT SCOPE_IDENTITY();", book);

			book.Id = id;
		}

		public async Task<IEnumerable<Book>> Get()
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryAsync<Book>($@"SELECT * FROM Books");
		}

		public async Task Update(Book book)
		{
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync($@"UPDATE Books
											SET Title = @Title,
												Description = @Description,
												PageCount = @PageCount,
												Excerpt = @Excerpt,
												PublishDate = @PublishDate
											WHERE Id = @Id", book);
		}

		public async Task<Book> GetById(int id)
		{
			using var connection = new SqlConnection(connectionString);
			return await connection.QueryFirstOrDefaultAsync<Book>($@"SELECT *
																	FROM Books
																	WHERE Id = @Id", new { id });

		}

		public async Task Delete(int id)
		{
			using var connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync($@"DELETE Books WHERE Id = @Id", new { id });
		}


	}
}
