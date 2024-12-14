using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetoveKnihkupectvi;

public interface IModelStore
{
    public IList<Book> GetBooks();
    public Book GetBook(int id);
    public Customer GetCustomer(int id);
    public Book? GetBookByCustAndId(Customer customer, int bookId);
}
