﻿using AutoMapper;
using BlazorBookHub.Server.Data;
using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .ToListAsync();

            return books.Select(b =>
            {
                var dto = _mapper.Map<BookDto>(b);
                dto.CategoryName = b.Category?.Name ?? string.Empty;
                return dto;
            });
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return null;

            var dto = _mapper.Map<BookDto>(book);
            dto.CategoryName = book.Category?.Name ?? string.Empty;
            return dto;
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description ?? string.Empty,
                CategoryId = dto.CategoryId,
                PdfPath = dto.PdfPath ?? string.Empty,
                CoverImagePath = dto.CoverImagePath ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateAsync(int id, CreateBookDto dto)
        {
            var existing = await _context.Books.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Book with id {id} not found.");

            existing.Title = dto.Title;
            existing.Author = dto.Author;
            existing.Description = dto.Description ?? string.Empty;
            existing.CategoryId = dto.CategoryId;

            if (!string.IsNullOrEmpty(dto.PdfPath))
                existing.PdfPath = dto.PdfPath;

            if (!string.IsNullOrEmpty(dto.CoverImagePath))
                existing.CoverImagePath = dto.CoverImagePath;

            _context.Books.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with id {id} not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Books.CountAsync();
        }
    }
}
