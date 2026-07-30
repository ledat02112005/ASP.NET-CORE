// Nạp thư viện hỗ trợ xử lý JSON của .NET
using System.Text.Json;

// Định nghĩa không gian tên (namespace) để các file khác có thể gọi đến
namespace SportsStore.WebUI.Infrastructure;

// Bắt buộc phải là class 'static' (tĩnh) để chứa các Extension Method
public static class SessionExtensions
{
    // Cú pháp 'this ISession session' giúp hàm SetJson gắn chặt vào mọi biến kiểu ISession.
    // 'key' là tên ngăn kéo trong Session, 'value' là món đồ (object) bạn muốn cất.
    public static void SetJson(this ISession session, string key, object value)
    {
        // Biến 'value' (ví dụ: giỏ hàng) thành một chuỗi văn bản định dạng JSON
        // Sau đó lưu chuỗi đó vào Session thông qua hàm SetString mặc định.
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    // '<T>' báo hiệu đây là hàm Generic (chấp nhận mọi kiểu dữ liệu).
    // Hàm này trả về kiểu T? (có thể null nếu không tìm thấy dữ liệu).
    public static T? GetJson<T>(this ISession session, string key)
    {
        // Lấy chuỗi JSON thô từ Session dựa vào 'key'
        var sessionData = session.GetString(key);

        // Toán tử 3 ngôi: Nếu chuỗi JSON bị rỗng (chưa từng lưu gì vào key này)
        return sessionData == null
            ? default(T)  // Trả về giá trị mặc định của T (thường là null đối với Object)
            // Nếu có chữ, dịch ngược chuỗi JSON đó thành Object kiểu T ban đầu
            : JsonSerializer.Deserialize<T>(sessionData);
    }
}
