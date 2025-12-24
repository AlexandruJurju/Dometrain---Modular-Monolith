using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users.Domain;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; private set; } = string.Empty;

    private readonly List<CartItem> _cartItems = [];
    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

    public void AddItemToCart(CartItem item)
    {
        Guard.Against.Null(item);

        var existingBook = _cartItems.SingleOrDefault(b => b.BookId == item.BookId);

        if (existingBook != null)
        {
            existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);
            return;
        }

        _cartItems.Add(item);
    }

    internal void ClearCart()
    {
        _cartItems.Clear();
    }
}