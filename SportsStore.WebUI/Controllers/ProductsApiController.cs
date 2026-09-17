// SportsStore.WebUI/Controllers/ProductsApiController.cs
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain;

namespace SportsStore.WebUI.Controllers;

[ApiController]
[Route("api/products")] // Định nghĩa base route cho controller này
public class ProductsApiController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsApiController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet] // Xử lý request GET /api/products
    public IQueryable<ModelProduct> GetProducts()
    {
        return _repository.Products;
    }
}
