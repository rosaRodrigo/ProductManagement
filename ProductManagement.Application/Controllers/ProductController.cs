using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTO;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain;
using ProductManagement.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Application.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<ProductDto>> GetProductById(int codProduto)
        {
            var product = await _productService.GetProductByIdAsync(codProduto);

            if (product == null)
            {
                return Ok("Nenhum produto encontrado.");
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("GetPagination")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductFilter filter, int page = 1, int pageSize = 10)
        {
            var products = await _productService.GetProductsAsync(filter, page, pageSize);

            return Ok(products);
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {

            await _productService.AddProductAsync(product);

            return Ok("Produto Adicionado com sucesso!");

        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductResponse product)
        {
            try
            {
                await _productService.UpdateProductAsync(product);
                return Ok("Produto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int codProduto)
        {
            try
            {
                await _productService.DeleteProductAsync(codProduto);
                return Ok("Produto inativado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
