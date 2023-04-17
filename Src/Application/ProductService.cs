using Cart.Application.DTOs;
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
        var att = new List<Product.Attachment>
        {
            new()
            {
                Alt = product.Attachments.FirstOrDefault()!.Alt,
                FileName = product.Attachments.FirstOrDefault()!.FileName,
                Title = product.Attachments.FirstOrDefault()!.Title
            }
        };
        var productMustAdd = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            Slug = product.Slug,
            Attachments = att
        };
        var rowAffected = await productRepository.AddProduct(productMustAdd);
        return rowAffected;
    }

    public async Task<int> DeleteProduct(int id) => await productRepository.DeleteProduct(id);

    public async Task UpdateProduct(int id, CreateEditProductDto product)
    {
        var att = new List<Product.Attachment>
        {
            new()
            {
                Alt = product.Attachments.FirstOrDefault()!.Alt,
                FileName = product.Attachments.FirstOrDefault()!.FileName,
                Title = product.Attachments.FirstOrDefault()!.Title
            }
        };
        var productMustEdit = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            Slug = product.Slug,
            Attachments = att
        };
        await productRepository.UpdateProduct(id, productMustEdit);
    }
}
