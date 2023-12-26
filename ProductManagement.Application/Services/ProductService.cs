using ProductManagement.Application.DTO;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain;
using ProductManagement.Domain.DTO;
using ProductManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int codProduto)
        {
            return await _productRepository.GetProductByIdAsync(codProduto);
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync(ProductFilter filter, int page, int pageSize)
        {

            var products = await _productRepository.GetProductsAsync(filter, page, pageSize);

            return products;
        }

        public async Task AddProductAsync(Product product)
        {
            // Validar a data de fabricação e validade
            if (product.DataFabricacao >= product.DataValidade)
            {
                throw new Exception("A data de fabricação não pode ser maior ou igual à data de validade.");
            }

            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(ProductResponse product)
        {
            // Validar a data de fabricação e validade
            if (product.DataFabricacao >= product.DataValidade)
            {
                throw new Exception("A data de fabricação não pode ser maior ou igual à data de validade.");
            }

            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int codProduto)
        {
            var product = await _productRepository.GetProductByIdAsync(codProduto);

            if (product != null)
            {
                product.Situacao = ProductStatus.Inativo; // Atualiza o status para Inativo
                await _productRepository.UpdateProductAsync(product);
            }
            else
            {
                new Exception("Produto não encontrado.");
            }
        }
    }
}
