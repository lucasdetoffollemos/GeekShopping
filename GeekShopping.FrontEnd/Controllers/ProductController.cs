using GeekShopping.FrontEnd.Models;
using GeekShopping.FrontEnd.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.FrontEnd.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel pm)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(pm);

                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(pm);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.FindProductById(id);

            if (product != null)
            {
                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel pm)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(pm);

                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(pm);
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();

            return View(products);
        }

        public async Task<IActionResult> ProductDelete(long id)
        {
            var product = await _productService.FindProductById(id);

            if (product != null)
            {
                return View(product);
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel pm)
        {
            var response = await _productService.DeleteProductById(pm.Id);

            if (response.Equals("Registro deletado com sucesso"))
            {
                return RedirectToAction(nameof(ProductIndex));
            }


            return View(pm);
        }

    }
}
