using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InternetoveKnihkupectvi;

public class NezarkaModel : IModelStore
{
    private List<Book> books = new List<Book>();
    private List<Customer> customers = new List<Customer>();

    public IList<Book> GetBooks()
    {
        return books;
    }

    public Book GetBook(int id)
    {
        return books.Find(b => b.Id == id)!;
    }

    public Customer GetCustomer(int id)
    {
        return customers.Find(c => c.Id == id)!;
    }

    public Book? GetBookByCustAndId(Customer customer, int bookId) 
    {
        var cartItem = customer.ShoppingCart.Items.Find(item => item.BookId == bookId);
        return GetBook(bookId);
    }

    public static NezarkaModel LoadFrom(TextReader reader)
    {
        var store = new NezarkaModel();

        if (reader.ReadLine() != "DATA-BEGIN")
        {
            throw new InvalidEShopDataException();
        }
        while (true)
        {
            string? line = reader.ReadLine();
            if (line == null)
            {
                throw new InvalidEShopDataException();
            }
            else if (line == "DATA-END")
            {
                break;
            }

            string[] tokens = line.Split(';');
            switch (tokens[0])
            {
                case "BOOK":
                    store.books.Add(new Book
                    {
                        Id = int.Parse(tokens[1]),
                        Title = tokens[2],
                        Author = tokens[3],
                        Price = decimal.Parse(tokens[4])
                    });
                    break;
                case "CUSTOMER":
                    store.customers.Add(new Customer
                    {
                        Id = int.Parse(tokens[1]),
                        FirstName = tokens[2],
                        LastName = tokens[3]
                    });
                    break;
                case "CART-ITEM":
                    var customer = store.GetCustomer(int.Parse(tokens[1]));
                    if (customer == null)
                    {
                        throw new InvalidEShopDataException();
                    }
                    customer.ShoppingCart.Items.Add(new ShoppingCartItem
                    {
                        BookId = int.Parse(tokens[2]),
                        Count = int.Parse(tokens[3])
                    });
                    break;
                default:
                    throw new InvalidEShopDataException();
            }
        }
        return store;
    }
}