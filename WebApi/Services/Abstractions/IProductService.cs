namespace Net7.WebApi.Test.Services.Abstractions
{
    public interface IProductService
    {
        public Task<List<TeapotResponse>?> GetProductsAsync();

        public Task<TeapotResponse?> GetProductByIdAsync(int id);

        public Task<bool> AddProductAsync(Teapot product);

        public Task<bool> EditProductAsync(int id, Teapot product);

        public Task<bool> DeleteProductByIdAsync(int id);
    }
}
