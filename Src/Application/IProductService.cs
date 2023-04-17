using Cart.Domain;
using Cart.Application.DTOs;

namespace Cart.Application;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(int id);
    Task<int> InsertProduct(CreateEditProductDto product);
    Task<int> DeleteProduct(int id);
    Task UpdateProduct(int id, CreateEditProductDto product);
}
