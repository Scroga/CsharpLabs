using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetoveKnihkupectvi;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public decimal Price { get; set; }
}

public class Customer
{
    private ShoppingCart? shoppingCart;

    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public ShoppingCart ShoppingCart
    {
        get
        {
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
            }
            return shoppingCart;
        }
        set
        {
            shoppingCart = value;
        }
    }

    public bool HasBook(int bookId) 
    {
        var shoppingItem = ShoppingCart.Items.Find(i => i.BookId == bookId);
        return shoppingItem != null;
    }
}

public class ShoppingCartItem
{
    public int BookId { get; set; }
    public int Count { get; set; }
}

public class ShoppingCart
{
    public int CustomerId { get; set; }
    public List<ShoppingCartItem> Items = new List<ShoppingCartItem>();

    public void AddItem(int bookId) 
    {
        var shoppingItem = Items.Find(i => i.BookId == bookId);
        if (shoppingItem == null)
        {

            Items.Add(new ShoppingCartItem
            {
                BookId = bookId,
                Count = 1
            });
        }
        else 
        {
            shoppingItem.Count++;
        }
    }
    public void RemoveItem(int bookId)
    {
        var shoppingItem = Items.Find(i => i.BookId == bookId);
        if (shoppingItem != null)
        {
            if (shoppingItem.Count > 1)
            {
                shoppingItem.Count--;
            }
            else 
            {
                Items.Remove(shoppingItem);
            }
        }
    }
}