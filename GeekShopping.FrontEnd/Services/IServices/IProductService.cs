using GeekShopping.FrontEnd.Models;

namespace GeekShopping.FrontEnd.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAllProducts();
        Task<ProductModel> FindProductById(long id);
        Task<ProductModel> CreateProduct(ProductModel model);
        Task<ProductModel> UpdateProduct(ProductModel model);
        Task<string> DeleteProductById(long id);
    }
}
