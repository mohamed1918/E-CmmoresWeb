using E_CmmoresWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CmmoresWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            return new Product() { Id = id };
        }

        [HttpPost]
        public ActionResult<Product> Add(Product product)
        {
            return product;
        }

        [HttpPut]
        public ActionResult<Product> Update(Product product)
        {
            return product;
        }

        [HttpDelete]
        public ActionResult<Product> Delete(Product product)
        {
            return product;
        }


    }
}
