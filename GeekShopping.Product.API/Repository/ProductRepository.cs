using AutoMapper;
using GeekShopping.Product.API.Data.ValueObjects;
using GeekShopping.Product.API.Model;
using GeekShopping.Product.API.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Product.API.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly MySQLContext _context;

        private readonly IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ProductVO> Create(ProductVO vo)
        {
            ProductEntity product = _mapper.Map<ProductEntity>(vo);

             _context.Products.Add(product);  

            await  _context.SaveChangesAsync();

            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            ProductEntity product = _mapper.Map<ProductEntity>(vo);

            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                ProductEntity product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id) ?? new ProductEntity();

                if(product == null || product.Id <= 0)
                    return false;

                _context.Products.Remove(product);

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<ProductEntity> products = await _context.Products.ToListAsync();

            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            ProductEntity product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id) ?? new ProductEntity();

            return _mapper.Map<ProductVO>(product);
        }

      
    }
}
