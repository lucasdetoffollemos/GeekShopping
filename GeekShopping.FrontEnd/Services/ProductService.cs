using GeekShopping.FrontEnd.Models;
using GeekShopping.FrontEnd.Services.IServices;
using GeekShopping.FrontEnd.Utils;
using System.Collections.Generic;

namespace GeekShopping.FrontEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client;   
        }
        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Somethig went wrong calling post product");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PutAsJson(BasePath, model);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Somethig went wrong calling put product");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<string> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Somethig went wrong calling delete product");

            return await response.ReadContentAs<string>();
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");

            return await response.ReadContentAs<ProductModel>();
        }
    }
}
