using Cart.Application.DTOs;
using Cart.Application.Handlers;
using Cart.Domain;
using Cart.Domain.Repositories;

namespace Cart.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;

    public ProductService(IProductRepository productRepository) =>
        this.productRepository = productRepository;

    public async Task<IEnumerable<Product>> GetProducts() => await productRepository.GetProducts();

    public async Task<Product> GetProduct(int id) => await productRepository.GetProduct(id);

    public async Task<int> InsertProduct(CreateEditProductDto product)
    {
        var rowAffected = await productRepository.AddProduct(new Product() { Name = product.Name, Description = product.Description, Quantity = product.Quantity, Slug = product.Slug, Attachments = ProductJsonHandler.Parse(product.Attachments) });
        return rowAffected;
    }

    public async Task<int> DeleteProduct(int id) => await productRepository.DeleteProduct(id);

    public async Task UpdateProduct(int id, CreateEditProductDto product)
    {
        await productRepository.UpdateProduct(id, new Product
        {
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            Slug = product.Slug,
            Attachments = ProductJsonHandler.Parse(product.Attachments)
        });
    }
}
