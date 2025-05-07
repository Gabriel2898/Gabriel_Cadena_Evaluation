using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTOs;
using ProductService.Helpers;
using ProductService.Interfaces;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;

        public ProductService(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductDTO>> GetProductsAsync(PaginationFilter paginationFilter)
        {
            var query = _context.Products.AsQueryable();


            var totalRecords = await query.CountAsync();
            var products = await query
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            var productDtos = _mapper.Map<List<ProductDTO>>(products);

            return new PagedResponse<ProductDTO>
            {
                Data = productDtos,
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> CreateProductAsync(CreateProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProductAsync(UpdateProductDTO productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);
            if (product == null) return false;

            _mapper.Map(productDto, product);
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;
            if (product.Stock + quantity < 0) return false;

            product.Stock += quantity;
            product.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}