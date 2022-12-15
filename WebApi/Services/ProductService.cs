using AutoMapper;
using Net7.WebApi.Test.Data;
using Net7.WebApi.Test.Providers.Abstractions;
using Net7.WebApi.Test.Services.Abstractions;

namespace Net7.WebApi.Test.Services
{
    public class ProductService : TransactionService<ApplicationDbContext>, IProductService
    {
        private readonly IProductProvider _productProvider;
        private readonly IMapper _mapper;

        public ProductService(IProductProvider productProvider, IMapper mapper, IDbContextWrapper<ApplicationDbContext> dbContextWrapper) 
            : base(dbContextWrapper)
        {
            _productProvider = productProvider;
            _mapper = mapper;
        }

        public async Task<List<TeapotResponse>?> GetProductsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var products = _mapper.Map<List<TeapotResponse>>(await _productProvider.GetProductsAsync());

                if (products.Count == 0)
                    return null;

                return products;
            });
            
        }

        public async Task<TeapotResponse?> GetProductByIdAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var product = _mapper.Map<TeapotResponse>(await _productProvider.GetProductByIdAsync(id));

                if (product == null)
                    return null;

                return product;
            });
        }

        public async Task<bool> AddProductAsync(Teapot product)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var productEntity = _mapper.Map<TeapotEntity>(product);
                bool isAdded = await _productProvider.AddProductAsync(productEntity);

                return isAdded;
            });
        }

        public async Task<bool> EditProductAsync(int id, Teapot product)
        {
            return await ExecuteSafeAsync(async () =>
            {
                bool isChanged = false;

                var productEntity = _mapper.Map<TeapotEntity>(product);
                productEntity.Id = id;

                isChanged = await _productProvider.UpdateProductAsync(productEntity);

                return isChanged;
            });
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                bool isRemoved = false;
                var recievedProduct = await _productProvider.GetProductByIdAsync(id);                

                if (recievedProduct != null)
                {
                    isRemoved = await _productProvider.DeleteProductAsync(recievedProduct);
                }

                return isRemoved;
            });
        }
    }
}
