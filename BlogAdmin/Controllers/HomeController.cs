﻿using BlogAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogAdmin.Filter;
namespace BlogAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _context;
        public HomeController(ILogger<HomeController> logger, BlogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult>Login(string email,string password)
        {
            var author = _context.Author.FirstOrDefault(w => w.Email == email && w.Password == password);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));

            }

            HttpContext.Session.SetInt32("id", author.Id);

            return RedirectToAction(nameof(Category));
        }

        //Category
        public async Task<IActionResult>AddCategory(Category category)
        {
            if (category.Id == 0) 
            { 
                await _context.AddAsync(category);
            }
            else
            {
                _context.Update(category);
            }
        
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Category));
        }
        public async Task <IActionResult> DeleteCategory(int? Id)
        {
            Category category = await _context.Category.FindAsync(Id);
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Category));
        }
        public async Task<IActionResult>CategoryDetails(int Id)
        {
            var category = await _context.Category.FindAsync(Id);
            return Json(category);
        }
        public IActionResult Category()
        {
            List<Category> list = _context.Category.ToList();
            return View(list);
        }
        public async Task<IActionResult> DeleteAuthor(int? Id)
        {
            var author = await _context.Author.FindAsync(Id);
            _context.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Author));
        }
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (author.Id == 0)
            {
                await _context.AddAsync(author);
            }
            else
            {
                _context.Update(author);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Author));
        }
        public async Task<IActionResult> AuthorDetails(int Id)
        {
            var author = await _context.Author.FindAsync(Id);
            return Json(author);
        }
        public IActionResult Author()
        {
            List<Author> list = _context.Author.ToList();
            return View(list);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
