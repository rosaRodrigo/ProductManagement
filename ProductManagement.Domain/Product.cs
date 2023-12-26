using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain
{
    public class Product
    {
        public int CodProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public ProductStatus Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJFornecedor { get; set; }

    }

    public enum ProductStatus
    {
        Ativo,
        Inativo
    }
}
