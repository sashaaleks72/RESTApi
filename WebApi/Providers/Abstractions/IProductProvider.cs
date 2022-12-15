namespace Net7.WebApi.Test.Providers.Abstractions
{
    public interface IProductProvider
    {
        public Task<List<TeapotEntity>> GetProductsAsync();

        public Task<TeapotEntity?> GetProductByIdAsync(int id);

        public Task<bool> AddProductAsync(TeapotEntity product);

        public Task<bool> UpdateProductAsync(TeapotEntity product);

        public Task<bool> DeleteProductAsync(TeapotEntity product);
    }
}
