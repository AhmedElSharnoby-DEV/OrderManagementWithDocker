using AutoMapper;
using IOrderManagementSystem.Dtos.ProductDtos;
using IOrderManagementSystem.Repositories.Abstractions;
using IOrderManagementSystem.Services.Abstractions;

namespace IOrderManagementSystem.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<GetProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<List<GetProductDto>>(products);
        }

        public async Task<GetProductDto?> GetProductByIdAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid product id");
            }
            var product = await _productRepository.GetProductByIdAsync(id);
            return _mapper.Map<GetProductDto>(product);
        }
    }
}
