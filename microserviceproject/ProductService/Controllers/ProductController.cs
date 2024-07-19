using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class ProductController : Controller
    {
        static List<Product> products = null;
        ILogger<ProductController> _logger;
        static readonly log4net.ILog _log4net =
            log4net.LogManager.GetLogger(typeof(ProductController));
        public ProductController(ILogger<ProductController> logger)
        {

            _logger = logger;
            if (products == null)
            {
                _log4net.Error("Pl provide data for products");
            }
            else
            {
                products = new List<Product>()
                {
                    new Product(){ Id=1,Name="Mouse", Description="Gray in color", QtyStock=9},

                    new Product(){ Id=2,Name="Scanner", Description="Gray in color", QtyStock=10}
                };
            }
        }
        [HttpGet]
        public List<Product> Get()
        {

            _logger.LogInformation("Inside Get Method");
            return products;
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            _logger.LogInformation("Inside Display Method");
            return products.FirstOrDefault(x => x.Id == id);
        }
        [HttpPost]
        public void AddProduct(Product product)
        {
            products.Add(product);
        }
        [HttpPut("{id}")]
        public void EditProduct(int id, Product product)
        {
            Product obj = products.FirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                foreach (Product temp in products)
                {
                    if (obj.Id == id)
                    {
                        obj.Description = product.Description;
                        obj.QtyStock = product.QtyStock;
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            Product obj = products.FirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                products.Remove(obj);

            }
        }

    }
}
