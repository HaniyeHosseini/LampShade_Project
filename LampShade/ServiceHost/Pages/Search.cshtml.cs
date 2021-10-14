using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        private readonly IProductQuery _productQuery;
        public List<ProductQueryModel> Products { get; set; }
        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = _productQuery.Search(value);


        }
    }
}
