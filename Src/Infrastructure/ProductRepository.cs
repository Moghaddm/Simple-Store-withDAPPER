using Dapper;
using Cart.Domain;
using System;
using Cart.Domain.Repositories;
using Newtonsoft.Json;
using Cart.Application.DTOs;

namespace Cart.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext context;

    public ProductRepository(StoreContext context) => this.context = context;

    public async ValueTask<Product> GetProduct(int id)
    {
        var query = "SELECT Name,Description,Quantity,Slug,attachments FROM PRODUCTS WHERE Id = @id";
        using (var connection = context.CreateConnection())
        {
            var product = await connection.QueryFirstOrDefaultAsync<CreateEditProductDto>(query, new { id });
        return new Product()
        {
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            Slug = product.Slug,
            Attachments = JsonConvert.DeserializeObject<List<Product.Attachment>>(product.Attachments)
        };
    }

    public async ValueTask<List<Product>> GetProducts()
    {
        var query = "SELECT * FROM PRODUCTS";
        using (var connection = context.CreateConnection())
        {
            var products = await connection.QueryAsync<Product>(query);
            return products.ToList();
        }
    }

    public async ValueTask<int> DeleteProduct(int id)
    {
        var query = "DELETE FROM PRODUCTS WHERE ID = @id";
        using (var connection = context.CreateConnection())
        {
            int rowAffected = await connection.ExecuteAsync(query, new { id });
            return rowAffected;
        }
    }

    public async ValueTask<int> AddProduct(Product product)
    {
        var query =
            "INSERT INTO PRODUCTS(NAME,DESCRIPTION,QUANTITY,SLUG) VALUES(@Name,@Description,@Quantity,@Slug)";
        using (var connection = context.CreateConnection())
        {
            int rowAffected = await connection.ExecuteAsync(query, product);
            return rowAffected;
        }
    }

    public async ValueTask UpdateProduct(int id, Product product)
    {
        var query =
            "UPDATE PRODUCTS SET NAME = @Name , DESCRIPTION = @Description , QUANTITY = @Quantity , SLUG = @Slug WHERE ID = @id";
        using (var connection = context.CreateConnection())
        {
            await connection.ExecuteAsync(
                query,
                new
                {
                    product.Name,
                    product.Description,
                    product.Quantity,
                    product.Slug
                }
            );
        }
    }
}
