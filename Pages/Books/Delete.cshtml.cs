﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Damean_Andrei_Stefan_Lab2.Data;
using Damean_Andrei_Stefan_Lab2.Models;

namespace Damean_Andrei_Stefan_Lab2.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly Damean_Andrei_Stefan_Lab2.Data.Damean_Andrei_Stefan_Lab2Context _context;

        public DeleteModel(Damean_Andrei_Stefan_Lab2.Data.Damean_Andrei_Stefan_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                 .Include(b => b.Author) // Ensure Author is included
                 .Include(b => b.Publisher) // Ensure Publisher is included
                 .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                Book = book;
                _context.Book.Remove(Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
