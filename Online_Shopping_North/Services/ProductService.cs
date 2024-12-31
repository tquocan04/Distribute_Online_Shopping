using AutoMapper;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepo _categoryRepo;

        public ProductService(IProductRepo productRepo, IMapper mapper, ICategoryRepo categoryRepo
            )
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }

        private async Task<ProductDTO> ConvertToProductDTO(Product product)
        {
            var cate = await _categoryRepo.GetCategoryByIdAsync(product.CategoryId);

            var prodDTO = _mapper.Map<ProductDTO>(product);

            prodDTO.CategoryName = cate.Name;

            return prodDTO;
        }

        private async Task<IEnumerable<ProductDTO>> GetProducts(IEnumerable<Product> products)
        {
            var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
            var list = products.ToList();
            var listDTO = productDTO.ToList();

            for (int i = 0; i < listDTO.Count(); i++)
            {
                listDTO[i] = await ConvertToProductDTO(list[i]);
            }

            productDTO = listDTO;
            return productDTO;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _productRepo.GetAllProductsAsync();

            return await GetProducts(products);
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            return await ConvertToProductDTO(await _productRepo.GetProductByIdAsync(Guid.Parse(id)));
        }

        public async Task DeleteProduct(string id)
        {
            var product = await _productRepo.GetProductByIdAsync(Guid.Parse(id));
            await _productRepo.DeleteProductAsync(product);

        }
    }
}
