using ProductManagement.Domain;
using ProductManagement.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> GetProductByIdAsync(int codProduto);
        Task<IEnumerable<ProductResponse>> GetProductsAsync(ProductFilter filter, int page, int pageSize);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(ProductResponse product);
        Task DeleteProductAsync(int codProduto);
    }
}
