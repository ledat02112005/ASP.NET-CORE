// SportsStore.Domain/Cart.cs

namespace SportsStore.Domain;

public class Cart
{
    // Danh sách các mặt hàng trong giỏ
    public List<CartLine> Lines { get; set; } = new List<CartLine>();

    // Thêm một sản phẩm vào giỏ hoặc tăng số lượng nếu đã tồn tại
    public virtual void AddItem(ModelProduct product, int quantity)
    {
        CartLine? line = Lines
            .Where(p => p.Product.ProductID == product.ProductID)
            .FirstOrDefault();

        if (line == null)
        {
            Lines.Add(new CartLine
            {
                Product = product,
                Quantity = quantity
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    // Xóa một sản phẩm khỏi giỏ
    public virtual void RemoveLine(ModelProduct product) =>
        Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);

    // Tính tổng giá trị giỏ hàng
    public decimal ComputeTotalValue() =>
        Lines.Sum(e => e.Product.Price * e.Quantity);

    // Xóa toàn bộ giỏ hàng
    public virtual void Clear() => Lines.Clear();
}

// Lớp đại diện cho một dòng trong giỏ hàng (một sản phẩm và số lượng của nó)
public class CartLine
{
    public int CartLineID { get; set; }
    public ModelProduct Product { get; set; } = null!;
    public int Quantity { get; set; }
}
