using BooksMVC.Models;
using BooksMVC.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BooksMVC.Controllers
{
	public class BooksController : Controller
	{
		private readonly IBookReposiroty bookReposiroty;

		public BooksController(IBookReposiroty bookReposiroty)
        {
			this.bookReposiroty = bookReposiroty;
		}

		public async Task<IActionResult> Index()
		{
			var books = await bookReposiroty.Get();
			return View(books);
		}

        public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(Book book)
		{
			if (!ModelState.IsValid)
			{
				return View(book);
			}

			await bookReposiroty.Create(book);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<ActionResult> Edit(int id)
		{
			var book = await bookReposiroty.GetById(id);

			if (book is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			return View(book);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(Book book)
		{
			var bookExits = await bookReposiroty.GetById(book.Id);

			if(bookExits is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			await bookReposiroty.Update(book);
			return RedirectToAction("Index");
		}


		public async Task<IActionResult> Delete(int id)
		{
			var book = await bookReposiroty.GetById(id);

			if (book is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			return View(book);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var book = await bookReposiroty.GetById(id);

			if (book is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			await bookReposiroty.Delete(id);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Details(int id)
		{
			var book = await bookReposiroty.GetById(id);

			if (book is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			return View(book);
		}


	}


}
