// SportsStore.WebUI/Models/SessionCart.cs

using SportsStore.Domain;
using SportsStore.WebUI.Infrastructure;
using System.Text.Json.Serialization;

namespace SportsStore.WebUI.Models;

public class SessionCart : Cart
{
    public static Cart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()
            .HttpContext?.Session;

        SessionCart cart = session?.GetJson<SessionCart>("Cart")
            ?? new SessionCart();
        cart.Session = session;
        return cart;
    }

    [JsonIgnore]
    public ISession? Session { get; set; }

    public override void AddItem(ModelProduct product, int quantity)
    {
        base.AddItem(product, quantity);
        Session?.SetJson("Cart", this);
    }

    public override void RemoveLine(ModelProduct product)
    {
        base.RemoveLine(product);
        Session?.SetJson("Cart", this);
    }

    public override void Clear()
    {
        base.Clear();
        Session?.SetJson("Cart", this);
    }
}
