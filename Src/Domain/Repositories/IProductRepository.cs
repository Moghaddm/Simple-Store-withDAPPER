namespace Cart.Domain.Repositories;

public interface IProductRepository
{
    ValueTask<Product> GetProduct(int id);
    ValueTask<List<Product>> GetProducts();
    ValueTask<int> DeleteProduct(int id);
    ValueTask<int> AddProduct(Product product);
    ValueTask UpdateProduct(int id, Product product);
}
