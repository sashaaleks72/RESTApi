using Microsoft.EntityFrameworkCore;
using Net7.WebApi.Test.Data;
using Net7.WebApi.Test.Providers.Abstractions;
using Net7.WebApi.Test.Services.Abstractions;

namespace Net7.WebApi.Test.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly IDbContextWrapper<ApplicationDbContext> _dbContextWrapper;

        public ProductProvider(IDbContextWrapper<ApplicationDbContext> dbContextWrapper) 
        {
            _dbContextWrapper = dbContextWrapper;
        }

        public async Task<List<TeapotEntity>> GetProductsAsync()
        {
            return await _dbContextWrapper.DbContext.Teapots.ToListAsync();
        }

        public async Task<TeapotEntity?> GetProductByIdAsync(int id)
        {
            return await _dbContextWrapper.DbContext.Teapots.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> AddProductAsync(TeapotEntity product)
        {
            await _dbContextWrapper.DbContext.AddAsync(product);
            int quantityOfAddedRows = await _dbContextWrapper.DbContext.SaveChangesAsync();

            return quantityOfAddedRows > 0;
        }

        public async Task<bool> UpdateProductAsync(TeapotEntity product)
        {
            _dbContextWrapper.DbContext.Teapots.Update(product);
            int quantityOfChangedRows = await _dbContextWrapper.DbContext.SaveChangesAsync();

            return quantityOfChangedRows > 0;
        }

        public async Task<bool> DeleteProductAsync(TeapotEntity product)
        {
            _dbContextWrapper.DbContext.Teapots.Remove(product);
            int quantityOfChangedRows = await _dbContextWrapper.DbContext.SaveChangesAsync();

            return quantityOfChangedRows > 0;
        }
    }
}
