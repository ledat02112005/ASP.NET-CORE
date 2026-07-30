// SportsStore.WebUI/Controllers/CartController.cs

using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain;
using SportsStore.WebUI.Infrastructure;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers;

public class CartController : Controller
{
    private IProductRepository _repository;
    private Cart cartService;

    public CartController(IProductRepository repo, Cart cart)
    {
        _repository = repo;
        cartService = cart;
    }

    // Action để xem chi tiết giỏ hàng
    public ViewResult Index(string returnUrl)
    {
        return View(new CartIndexViewModel
        {
            Cart = cartService,
            ReturnUrl = returnUrl ?? "/"
        });
    }

    // Action để thêm sản phẩm vào giỏ
    [HttpPost]
    public RedirectToActionResult AddToCart(int productId, string returnUrl)
    {
        ModelProduct? product = _repository.Products
            .FirstOrDefault(p => p.ProductID == productId);

        if (product != null)
        {
            cartService.AddItem(product, 1);
        }

        return RedirectToAction("Index", new { returnUrl });
    }

    // Action để xóa sản phẩm khỏi giỏ
    [HttpPost]
    public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
    {
        ModelProduct? product = _repository.Products
            .FirstOrDefault(p => p.ProductID == productId);

        if (product != null)
        {
            cartService.RemoveLine(product);
        }

        return RedirectToAction("Index", new { returnUrl });
    }
}
