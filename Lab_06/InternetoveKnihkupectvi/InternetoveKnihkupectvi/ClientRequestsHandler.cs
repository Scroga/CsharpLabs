using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace InternetoveKnihkupectvi;

public class ClientRequestsHandler : IRequestsHandler
{
    IModelStore _modelData;
    HtmlToConsoleWriter _writer;

    const string _requiredUrlStart   = "http://www.nezarka.net/";
    const string _Books              = _requiredUrlStart + "Books";
    const string _BooksDetail        = _Books + "/Detail/";
    const string _ShoppingCart       = _requiredUrlStart + "ShoppingCart";
    const string _ShoppingCartAdd    = _ShoppingCart + "/Add/";
    const string _ShoppingCartRemove = _ShoppingCart + "/Remove/";

    public ClientRequestsHandler(IModelStore store, HtmlToConsoleWriter writer) 
    {
        _modelData = store;
        _writer = writer;
    }

    public void ProcessRequest(Request request)
    {
        if (!VerifyRequest(request)) 
        {
            _writer.InvalidRequestPage();
            return;
        }

        string url = request.Url;
        var customer = _modelData.GetCustomer(request.CustId);

        if (url.StartsWith(_Books))
        {
            if (url == _Books)
            {
                var books = _modelData.GetBooks();
                _writer.BooksTablePage(customer, books);

                return;
            }
            else if (url.StartsWith(_BooksDetail))
            {
                string bookId = url.Substring(_BooksDetail.Length);
                if (int.TryParse(bookId, out int id))
                {
                    var book = _modelData.GetBookByCustAndId(customer, id);
                    _writer.BookDetails(customer, book!);
                    return;
                }
            }
        }
        else if (url.StartsWith(_ShoppingCart))
        {
            if(url == _ShoppingCart)
            {
                WriteShoppingCartDetails(customer);
                return;
            }
            else if (url.StartsWith(_ShoppingCartAdd))
            {
                string bookId = url.Substring(_ShoppingCartAdd.Length);
                if (int.TryParse(bookId, out int id))
                {
                    if (_modelData.GetBook(id) != null)
                    {
                        customer.ShoppingCart.AddItem(id);
                        WriteShoppingCartDetails(customer);
                        return;
                    }
                }
            }
            else if (url.StartsWith(_ShoppingCartRemove))
            {
                string bookId = url.Substring(_ShoppingCartRemove.Length);
                if (int.TryParse(bookId, out int id))
                {
                    if (_modelData.GetBook(id) != null && customer.HasBook(id))
                    {
                        customer.ShoppingCart.RemoveItem(id);
                        WriteShoppingCartDetails(customer);
                        return;
                    }
                }
            }
        }

        _writer.InvalidRequestPage();
        return;
    }

    private void WriteShoppingCartDetails(Customer customer) 
    {
        var shoppingCart = customer.ShoppingCart;
        if (shoppingCart.Items.Count == 0)
        {
            _writer.EmptyShoppingCart(customer);
        }
        else
        {
            _writer.ShoppingCartDetails(customer, shoppingCart, _modelData.GetBooks());
        }
    }
    
    private bool VerifyRequest(Request request) 
    {
        return request.Type == RequestType.Valid &&
               request.Url.Length > 3 &&
               request.Url.StartsWith(_requiredUrlStart) &&
               _modelData.GetCustomer(request.CustId) != null;
    }

    public void Finish() 
    {
        _writer.Dispose();
    }
}
