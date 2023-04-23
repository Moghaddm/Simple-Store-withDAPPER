using Cart.Application.DTOs;
using Cart.Application;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService products;

    public ProductController(ProductService products) => this.products = products;

    [HttpGet(Name = nameof(GetAll))]
    public async Task<IActionResult> GetAll() => Ok(await products.GetProducts());

    [HttpGet("{id}", Name = "GetById")]
    public async Task<IActionResult> Get(int id) => Ok(await products.GetProduct(id));

    [HttpPost("{action}", Name = nameof(Insert))]
    public async Task<IActionResult> Insert(CreateEditProductDto product)
    {
        try
        {
            if (product is null)
                return BadRequest("Please Enter Product to ADD!");
            await products.InsertProduct(
                new CreateEditProductDto()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Slug = product.Slug,
                    Attachments = product.Attachments
                }
            );
            return CreatedAtRoute(nameof(Get), new { id = product.Id }, product);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPatch("{action}/{id}", Name = nameof(Update))]
    public async Task<IActionResult> Update(int id, CreateEditProductDto inputProduct)
    {
        try
        {
            if (products.GetProduct(id) is null)
            {
                return NotFound("Your Product Does Not Exist!");
            }
            if (inputProduct is null)
            {
                return BadRequest("Please Enter Product to Edit!");
            }
            var product = new CreateEditProductDto
            {
                Name = inputProduct.Name,
                Description = inputProduct.Description,
                Quantity = inputProduct.Quantity,
                Slug = inputProduct.Slug,
                Attachments = inputProduct.Attachments
            };
            await products.UpdateProduct(id, product);
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    public async Task<IActionResult> Delete(int id) => Ok(await products.DeleteProduct(id));
}
