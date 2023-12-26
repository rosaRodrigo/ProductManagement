using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManagement.Domain;
using ProductManagement.Domain.DTO;
using ProductManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int codProduto)
        {

            var connection = _configuration.GetConnectionString("DapperConnection");

            using (var context = new SqlConnection(connection))
            {
                var querySql = @$"SELECT * FROM Produto where codProduto = '{codProduto}'";

                var products = await context.QueryFirstOrDefaultAsync<ProductResponse>(querySql, new { CodProduto = codProduto });

                return products;

            }
        }


        public async Task<IEnumerable<ProductResponse>> GetProductsAsync(ProductFilter filter, int page, int pageSize)
        {
            var connection = _configuration.GetConnectionString("DapperConnection");

            using (var context = new SqlConnection(connection))
            {
                var sql = new StringBuilder();
                sql.Append("SELECT * FROM Produto WHERE 1 = 1");

                // Adiciona filtros
                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Descricao))
                    {
                        sql.Append(" AND DescricaoProduto LIKE @Descricao");
                    }

                    if (!string.IsNullOrEmpty(filter.Situacao))
                    {
                        sql.Append(" AND Situacao = @Situacao");
                    }
                }

                sql.Append(" ORDER BY CodProduto");

                // Adiciona paginação
                sql.Append($" OFFSET {pageSize * (page - 1)} ROWS FETCH NEXT {pageSize} ROWS ONLY");

                var parameters = new
                {
                    Descricao = $"%{filter?.Descricao}%",
                    Situacao = $"{filter?.Situacao}"
                };

                var products = (await context.QueryAsync<ProductResponse>(sql.ToString(), parameters)).ToList();
                return products;
            }
        }

        public async Task AddProductAsync(Product product)
        {

            var connection = _configuration.GetConnectionString("DapperConnection");

            using (var context = new SqlConnection(connection))
            {
                var sql = "INSERT INTO Produto (DescricaoProduto, Situacao, DataFabricacao, DataValidade, CodFornecedor, DescricaoFornecedor, CNPJFornecedor) " +
                          "VALUES (@DescricaoProduto, @Situacao, @DataFabricacao, @DataValidade, @CodFornecedor, @DescricaoFornecedor, @CNPJFornecedor);";

                var parameters = new
                {
                    DescricaoProduto = product.DescricaoProduto,
                    Situacao = product.Situacao,
                    DataFabricacao = product.DataFabricacao,
                    DataValidade = product.DataValidade,
                    CodFornecedor = product.CodFornecedor,
                    DescricaoFornecedor = product.DescricaoFornecedor,
                    CNPJFornecedor = product.CNPJFornecedor
                };

                await context.ExecuteAsync(sql, parameters);
            }
        }


        public async Task UpdateProductAsync(ProductResponse product)
        {
            var connection = _configuration.GetConnectionString("DapperConnection");

            using (var context = new SqlConnection(connection))
            {
                var sql = "UPDATE Produto SET " +
                          "DescricaoProduto = @DescricaoProduto, " +
                          "Situacao = @Situacao, " +
                          "DataFabricacao = @DataFabricacao, " +
                          "DataValidade = @DataValidade, " +
                          "CodFornecedor = @CodFornecedor, " +
                          "DescricaoFornecedor = @DescricaoFornecedor, " +
                          "CNPJFornecedor = @CNPJFornecedor " +
                          "WHERE CodProduto = @CodProduto";

                var parameters = new
                {
                    CodProduto = product.CodProduto,
                    DescricaoProduto = product.DescricaoProduto,
                    Situacao = product.Situacao,
                    DataFabricacao = product.DataFabricacao,
                    DataValidade = product.DataValidade,
                    CodFornecedor = product.CodFornecedor,
                    DescricaoFornecedor = product.DescricaoFornecedor,
                    CNPJFornecedor = product.CNPJFornecedor
                };

                await context.ExecuteAsync(sql, parameters);
            }
        }
    }
}
