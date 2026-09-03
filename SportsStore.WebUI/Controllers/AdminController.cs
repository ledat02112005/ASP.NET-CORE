// SportsStore.WebUI/Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain;

namespace SportsStore.WebUI.Controllers;

public class AdminController : Controller
{
    private readonly IProductRepository _repository;

    public AdminController(IProductRepository repo)
    {
        _repository = repo;
    }

    // Action để hiển thị danh sách sản phẩm
    public ViewResult Index() => View(_repository.Products);

    // Action GET để hiển thị form tạo mới (dùng chung View "Edit" với model rỗng)
    public ViewResult Create() => View("Edit", new ModelProduct { Name = "", Description = "", Category = "" });

    // Action POST để xử lý việc tạo mới
    [HttpPost]
    public async Task<IActionResult> Create(ModelProduct product, IFormFile? image)
    {
        if (ModelState.IsValid)
        {
            if (image != null)
            {
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                product.ImageUrl = "/images/" + image.FileName;
            }

            _repository.SaveProduct(product);
            TempData["message"] = $"{product.Name} has been saved";
            return RedirectToAction("Index");
        }

        // Nếu có lỗi, trả về view với dữ liệu đã nhập
        return View("Edit", product);
    }

    // Action GET để lấy sản phẩm và hiển thị form edit
    public ViewResult Edit(int productId) =>
        View(_repository.Products.FirstOrDefault(p => p.ProductID == productId));

    // Action POST để xử lý việc cập nhật
    [HttpPost]
    public async Task<IActionResult> Edit(ModelProduct product, IFormFile? image)
    {
        if (ModelState.IsValid)
        {
            if (image != null)
            {
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                product.ImageUrl = "/images/" + image.FileName;
            }

            _repository.SaveProduct(product);
            TempData["message"] = $"{product.Name} has been saved";
            return RedirectToAction("Index");
        }

        return View(product);
    }

    // Action POST để xử lý việc xóa
    [HttpPost]
    public IActionResult Delete(int productID)
    {
        ModelProduct? deletedProduct = _repository.DeleteProduct(productID);

        if (deletedProduct != null)
        {
            // Xóa file ảnh liên quan trên server
            if (!string.IsNullOrEmpty(deletedProduct.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    deletedProduct.ImageUrl.TrimStart('/'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            TempData["message"] = $"{deletedProduct.Name} was deleted";
        }

        return RedirectToAction("Index");
    }
}
