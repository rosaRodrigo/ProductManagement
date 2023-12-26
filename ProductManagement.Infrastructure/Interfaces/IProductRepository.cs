using ProductManagement.Domain;
using ProductManagement.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductResponse> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductResponse>> GetProductsAsync(ProductFilter filter, int page, int pageSize);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(ProductResponse product);
    }
}
