using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IProductRepository _repository;

    public HomeController(IProductRepository repository)
    {
        _repository = repository;
    }

    public int PageSize = 3;

    public IActionResult Index(string? category, int page = 1)
    {
        var products = _repository.Products;

        if (!string.IsNullOrEmpty(category))
        {
            products = products.Where(p => p.Category == category);
        }

        var totalItems = products.Count();

        var viewModel = new ProductsListViewModel
        {
            Products = products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList(),
            PagingInfo = new PagingInfo
            {
                TotalItems = totalItems,
                ItemsPerPage = PageSize,
                CurrentPage = page
            },
            CurrentCategory = category
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
