using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain;

namespace SportsStore.WebUI.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            // Lấy danh sách category không trùng lặp, sắp xếp theo tên
            var categories = _repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            // Đọc category từ query string hoặc route data
            var selectedCategory = HttpContext.Request.Query["category"].ToString();
            if (string.IsNullOrEmpty(selectedCategory))
                selectedCategory = RouteData?.Values["category"]?.ToString();

            ViewBag.SelectedCategory = selectedCategory;

            return View(categories);
        }
    }
}
