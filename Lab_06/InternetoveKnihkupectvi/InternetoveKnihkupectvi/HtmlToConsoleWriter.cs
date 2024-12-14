using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InternetoveKnihkupectvi;

public class HtmlToConsoleWriter : IDisposable
{
    TextWriter _writer;
    bool _disposed = false;

    public HtmlToConsoleWriter(TextWriter writer) 
    {
        _writer = writer;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _writer?.Dispose();
            _disposed = true;
        }
    }

    private void WriteLine(string text)
    {
        _writer.WriteLine(text);
    }
    private void WriteHeader() 
    {
        _writer.WriteLine("<!DOCTYPE html>");
        _writer.WriteLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
        _writer.WriteLine("<head>");
        _writer.WriteLine("\t<meta charset=\"utf-8\" />");
        _writer.WriteLine("\t<title>Nezarka.net: Online Shopping for Books</title>");
        _writer.WriteLine("</head>");
        _writer.WriteLine("<body>");
    }
    private void WriteEnd()
    {
        _writer.WriteLine("</body>");
        _writer.WriteLine("</html>");
        _writer.WriteLine("====");
    }
    private void WriteStyle() 
    {
        _writer.WriteLine("\t<style type=\"text/css\">");
        _writer.WriteLine("\t\ttable, th, td {");
        _writer.WriteLine("\t\t\tborder: 1px solid black;");
        _writer.WriteLine("\t\t\tborder-collapse: collapse;");
        _writer.WriteLine("\t\t}");
        _writer.WriteLine("\t\ttable {");
        _writer.WriteLine("\t\t\tmargin-bottom: 10px;");
        _writer.WriteLine("\t\t}");
        _writer.WriteLine("\t\tpre {");
        _writer.WriteLine("\t\t\tline-height: 70%;");
        _writer.WriteLine("\t\t}");
        _writer.WriteLine("\t</style>");
    }
    private void WriteCustInfo(Customer customer) 
    {
        _writer.WriteLine("\t<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>");
        _writer.WriteLine($"\t{customer.FirstName}, here is your menu:");

        _writer.WriteLine("\t<table>");
        _writer.WriteLine("\t\t<tr>");
        _writer.WriteLine("\t\t\t<td><a href=\"/Books\">Books</a></td>");
        _writer.WriteLine($"\t\t\t<td><a href=\"/ShoppingCart\">Cart ({customer.ShoppingCart.Items.Count})</a></td>");
        _writer.WriteLine("\t\t</tr>");
        _writer.WriteLine("\t</table>");
    }
    private void WriteBookTable(IList<Book> books) 
    {
        _writer.WriteLine("\tOur books for you:");
        if (books.Count == 0)
        {
            _writer.WriteLine("\t<table>");
            _writer.WriteLine("\t</table>");
            return;
        }

        _writer.WriteLine("\t<table>");
        for (int i = 0; i < books.Count; i++)
        {
            if (i % 3 == 0)
            {
                if (i > 0) _writer.WriteLine("\t\t</tr>");
                _writer.WriteLine("\t\t<tr>");
            }

            var book = books[i];
            _writer.WriteLine("\t\t\t<td style=\"padding: 10px;\">");
            _writer.WriteLine($"\t\t\t\t<a href=\"/Books/Detail/{book.Id}\">{book.Title}</a><br />");
            _writer.WriteLine($"\t\t\t\tAuthor: {book.Author}<br />");
            _writer.WriteLine($"\t\t\t\tPrice: {book.Price} EUR &lt;<a href=\"/ShoppingCart/Add/{book.Id}\">Buy</a>&gt;");
            _writer.WriteLine("\t\t\t</td>");
        }

        if (books.Count % 3 != 0)
        {
            _writer.WriteLine("\t\t</tr>");
        }

        _writer.WriteLine("\t</table>");
    }
    private void WriteBookDetails(Book book) 
    {
        _writer.WriteLine("\tBook details:");
        if (book == null) return;

        _writer.WriteLine($"\t<h2>{book.Title}</h2>");
        _writer.WriteLine("\t<p style=\"margin-left: 20px\">");
        _writer.WriteLine($"\tAuthor: {book.Author}<br />");
        _writer.WriteLine($"\tPrice: {book.Price} EUR<br />");
        _writer.WriteLine("\t</p>");
        _writer.WriteLine($"\t<h3>&lt;<a href=\"/ShoppingCart/Add/{book.Id}\">Buy this book</a>&gt;</h3>");
    }
    private void WriteShoppingCartDetails(ShoppingCart cart, IList<Book> books) 
    {
        _writer.WriteLine("\tYour shopping cart:");
        _writer.WriteLine("\t<table>");

        _writer.WriteLine("\t\t<tr>");
        _writer.WriteLine("\t\t\t<th>Title</th>");
        _writer.WriteLine("\t\t\t<th>Count</th>");
        _writer.WriteLine("\t\t\t<th>Price</th>");
        _writer.WriteLine("\t\t\t<th>Actions</th>");
        _writer.WriteLine("\t\t</tr>");

        int totalPrice = 0;
        foreach (ShoppingCartItem item in cart.Items) 
        {
            var book = books.FirstOrDefault(b => b.Id == item.BookId);
            if (book == null) { continue; }
            int sumOfPrices = (item.Count * (int)book.Price);
            totalPrice += sumOfPrices;

            _writer.WriteLine("\t\t<tr>");

            _writer.WriteLine($"\t\t\t<td><a href=\"/Books/Detail/{item.BookId}\">{book.Title}</a></td>");
            _writer.WriteLine($"\t\t\t<td>{item.Count}</td>");

            if (item.Count > 1)
            {
                _writer.WriteLine($"\t\t\t<td>{item.Count} * {book.Price} = {sumOfPrices} EUR</td>");
            }
            else
            {
                _writer.WriteLine($"\t\t\t<td>{sumOfPrices} EUR</td>");
            }
            _writer.WriteLine($"\t\t\t<td>&lt;<a href=\"/ShoppingCart/Remove/{item.BookId}\">Remove</a>&gt;</td>");

            _writer.WriteLine("\t\t</tr>");
        }

        _writer.WriteLine("\t</table>");
        _writer.WriteLine($"\tTotal price of all items: {totalPrice} EUR");
    }
     
    public void InvalidRequestPage()
    {
        WriteHeader();
        WriteLine("<p>Invalid request.</p>");
        WriteEnd();
    }
    public void BooksTablePage(Customer customer, IList<Book> books)
    {
        WriteHeader();
        WriteStyle();
        WriteCustInfo(customer);
        WriteBookTable(books);
        WriteEnd();
    }
    public void BookDetails(Customer customer, Book book) 
    {
        WriteHeader();
        WriteStyle();
        WriteCustInfo(customer);
        WriteBookDetails(book);
        WriteEnd();
    }
    public void EmptyShoppingCart(Customer customer)
    {
        WriteHeader();
        WriteStyle();
        WriteCustInfo(customer);
        WriteLine("\tYour shopping cart is EMPTY.");
        WriteEnd();
    }
    public void ShoppingCartDetails(Customer customer, ShoppingCart cart, IList<Book> books) 
    {
        WriteHeader();
        WriteStyle();
        WriteCustInfo(customer);
        WriteShoppingCartDetails(cart, books);
        WriteEnd();
    }
}
