using Microsoft.EntityFrameworkCore;
using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeDefaultCategories()
        {
            var defaultCategories = new[]
            {
                new Models.Category
                {
                    Name = "Bilim Kurgu",
                    Description = "Bilim Kurgu film türü",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedById = 1
                },
                new Models.Category
                {
                    Name = "Komedi",
                    Description = "Komedi film türü",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedById = 1
                },
                new Models.Category
                {
                    Name = "Dram",
                    Description = "Dram film türü",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedById = 1
                },
                new Models.Category
                {
                    Name = "Aksiyon",
                    Description = "Aksiyon film türü",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedById = 1
                }
            };

            foreach (var category in defaultCategories)
            {
                if (!await _context.Categories.AnyAsync(c => c.Name == category.Name))
                {
                    await _context.Categories.AddAsync(category);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddCategory(string name, string description)
        {
            try
            {
                var category = new Models.Category
                {
                    Name = name,
                    Description = description,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedById = 1 
                };

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategory(int id, string name, string description)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null) return false;

                category.Name = name;
                category.Description = description;
                category.UpdatedDate = DateTime.Now;
                category.UpdatedById = 1; 

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null) return false;

                category.IsDeleted = true;
                category.UpdatedDate = DateTime.Now;
                category.UpdatedById = 1;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Models.Category>> GetAllCategories()
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Models.Category> GetCategoryById(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }
    }
}
