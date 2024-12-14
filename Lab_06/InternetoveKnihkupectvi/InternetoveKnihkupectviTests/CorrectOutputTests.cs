using InternetoveKnihkupectvi;

namespace InternetoveKnihkupectviTests;

public class TestsWithExpectedOutputData
{
    const string inputStoreData = """
                                  DATA-BEGIN
                                  BOOK;1;Lord of the Rings;J. R. R. Tolkien;59
                                  BOOK;2;Hobbit: There and Back Again;J. R. R. Tolkien;49
                                  BOOK;3;Going Postal;Terry Pratchett;28
                                  BOOK;4;The Colour of Magic;Terry Pratchett;159
                                  BOOK;5;I Shall Wear Midnight;Terry Pratchett;31
                                  CUSTOMER;1;Pavel;Jezek
                                  CUSTOMER;2;Jan;Kofron
                                  CUSTOMER;3;Petr;Hnetynka
                                  CUSTOMER;4;Tomas;Bures
                                  CART-ITEM;2;1;3
                                  CART-ITEM;2;5;1
                                  DATA-END
                                  """;

    void TestProcess(string inputRequest, StringWriter outputWriter)
    {
        var storeReader = new StringReader(inputStoreData);
        var requestReader = new StringReader(inputRequest);

        var store = NezarkaModel.LoadFrom(storeReader);
        var writer = new HtmlToConsoleWriter(outputWriter);

        var handler = new ClientRequestsHandler(store, writer);
        var reader = new ClientRequestsReader(requestReader);

        RequestsProcessing.ProcessRequests(handler, reader);
    }

    [Fact]
    public void CustID_1_Books()
    {
        var outputWriter = new StringWriter();

        string inputRequest = """
                               GET 1 http://www.nezarka.net/Books
                               """;

        string expectedHTML = """
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Pavel, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (0)</a></td>
                              		</tr>
                              	</table>
                              	Our books for you:
                              	<table>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/1">Lord of the Rings</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 59 EUR &lt;<a href="/ShoppingCart/Add/1">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/2">Hobbit: There and Back Again</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 49 EUR &lt;<a href="/ShoppingCart/Add/2">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/3">Going Postal</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 28 EUR &lt;<a href="/ShoppingCart/Add/3">Buy</a>&gt;
                              			</td>
                              		</tr>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/4">The Colour of Magic</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 159 EUR &lt;<a href="/ShoppingCart/Add/4">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/5">I Shall Wear Midnight</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 31 EUR &lt;<a href="/ShoppingCart/Add/5">Buy</a>&gt;
                              			</td>
                              		</tr>
                              	</table>
                              </body>
                              </html>
                              ====

                              """;

        TestProcess(inputRequest, outputWriter);

        string actualHTM = outputWriter.ToString();
        Assert.Equal(expectedHTML, actualHTM);
    }

    [Fact]
    public void CustID_2_Books()
    {
        var outputWriter = new StringWriter();

        string inputRequest = """
                               GET 2 http://www.nezarka.net/Books
                               """;

        string expectedHTML = """
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (2)</a></td>
                              		</tr>
                              	</table>
                              	Our books for you:
                              	<table>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/1">Lord of the Rings</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 59 EUR &lt;<a href="/ShoppingCart/Add/1">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/2">Hobbit: There and Back Again</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 49 EUR &lt;<a href="/ShoppingCart/Add/2">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/3">Going Postal</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 28 EUR &lt;<a href="/ShoppingCart/Add/3">Buy</a>&gt;
                              			</td>
                              		</tr>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/4">The Colour of Magic</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 159 EUR &lt;<a href="/ShoppingCart/Add/4">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/5">I Shall Wear Midnight</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 31 EUR &lt;<a href="/ShoppingCart/Add/5">Buy</a>&gt;
                              			</td>
                              		</tr>
                              	</table>
                              </body>
                              </html>
                              ====

                              """;

        TestProcess(inputRequest, outputWriter);

        string actualHTM = outputWriter.ToString();
        Assert.Equal(expectedHTML, actualHTM);
    }

    [Fact]
    public void CustID_2_Books_Detail_BookID_3()
    {
        var outputWriter = new StringWriter();

        string inputRequest = """
                               GET 2 http://www.nezarka.net/Books/Detail/3
                               """;

        string expectedHTML = """
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (2)</a></td>
                              		</tr>
                              	</table>
                              	Book details:
                              	<h2>Going Postal</h2>
                              	<p style="margin-left: 20px">
                              	Author: Terry Pratchett<br />
                              	Price: 28 EUR<br />
                              	</p>
                              	<h3>&lt;<a href="/ShoppingCart/Add/3">Buy this book</a>&gt;</h3>
                              </body>
                              </html>
                              ====

                              """;

        TestProcess(inputRequest, outputWriter);

        string actualHTM = outputWriter.ToString();
        Assert.Equal(expectedHTML, actualHTM);
    }

    [Fact]
    public void CustID_2_ShoppingCart_Add_BookID_3()
    {
        var outputWriter = new StringWriter();

        string inputRequest = """
                              GET 2 http://www.nezarka.net/ShoppingCart/Add/3
                              """;

        string expectedHTML = """
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (3)</a></td>
                              		</tr>
                              	</table>
                              	Your shopping cart:
                              	<table>
                              		<tr>
                              			<th>Title</th>
                              			<th>Count</th>
                              			<th>Price</th>
                              			<th>Actions</th>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/1">Lord of the Rings</a></td>
                              			<td>3</td>
                              			<td>3 * 59 = 177 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/1">Remove</a>&gt;</td>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/5">I Shall Wear Midnight</a></td>
                              			<td>1</td>
                              			<td>31 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/5">Remove</a>&gt;</td>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/3">Going Postal</a></td>
                              			<td>1</td>
                              			<td>28 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/3">Remove</a>&gt;</td>
                              		</tr>
                              	</table>
                              	Total price of all items: 236 EUR
                              </body>
                              </html>
                              ====

                              """;

        TestProcess(inputRequest, outputWriter);

        string actualHTM = outputWriter.ToString();
        Assert.Equal(expectedHTML, actualHTM);
    }

    [Fact]
    public void AllProgrammTest()
    {
        var outputWriter = new StringWriter();

        string inputRequest = """
                              GET 1 http://www.nezarka.net/Books
                              GET 2 http://www.nezarka.net/Books
                              GET 2 http://www.nezarka.net/Books/Detail/3
                              GET 2 http://www.nezarka.net/ShoppingCart/Add/3
                              GET 1 http://www.nezarka.net/ShoppingCart
                              GET 1 http://www.nezarka.net/Books/Detail/4
                              GET 1 http://www.nezarka.net/ShoppingCart/Add/4
                              GET 1 http://www.nezarka.net/Books
                              GET 2 http://www.nezarka.net/ShoppingCart/Remove/2
                              GET 2 http://www.nezarka.net/ShoppingCart/Remove/1
                              GET 2 http://www.nezarka.net/ShoppingCart/Remove/5
                              """;

        string expectedHTML = """
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Pavel, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (0)</a></td>
                              		</tr>
                              	</table>
                              	Our books for you:
                              	<table>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/1">Lord of the Rings</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 59 EUR &lt;<a href="/ShoppingCart/Add/1">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/2">Hobbit: There and Back Again</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 49 EUR &lt;<a href="/ShoppingCart/Add/2">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/3">Going Postal</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 28 EUR &lt;<a href="/ShoppingCart/Add/3">Buy</a>&gt;
                              			</td>
                              		</tr>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/4">The Colour of Magic</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 159 EUR &lt;<a href="/ShoppingCart/Add/4">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/5">I Shall Wear Midnight</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 31 EUR &lt;<a href="/ShoppingCart/Add/5">Buy</a>&gt;
                              			</td>
                              		</tr>
                              	</table>
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (2)</a></td>
                              		</tr>
                              	</table>
                              	Our books for you:
                              	<table>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/1">Lord of the Rings</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 59 EUR &lt;<a href="/ShoppingCart/Add/1">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/2">Hobbit: There and Back Again</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 49 EUR &lt;<a href="/ShoppingCart/Add/2">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/3">Going Postal</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 28 EUR &lt;<a href="/ShoppingCart/Add/3">Buy</a>&gt;
                              			</td>
                              		</tr>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/4">The Colour of Magic</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 159 EUR &lt;<a href="/ShoppingCart/Add/4">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/5">I Shall Wear Midnight</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 31 EUR &lt;<a href="/ShoppingCart/Add/5">Buy</a>&gt;
                              			</td>
                              		</tr>
                              	</table>
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (2)</a></td>
                              		</tr>
                              	</table>
                              	Book details:
                              	<h2>Going Postal</h2>
                              	<p style="margin-left: 20px">
                              	Author: Terry Pratchett<br />
                              	Price: 28 EUR<br />
                              	</p>
                              	<h3>&lt;<a href="/ShoppingCart/Add/3">Buy this book</a>&gt;</h3>
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (3)</a></td>
                              		</tr>
                              	</table>
                              	Your shopping cart:
                              	<table>
                              		<tr>
                              			<th>Title</th>
                              			<th>Count</th>
                              			<th>Price</th>
                              			<th>Actions</th>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/1">Lord of the Rings</a></td>
                              			<td>3</td>
                              			<td>3 * 59 = 177 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/1">Remove</a>&gt;</td>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/5">I Shall Wear Midnight</a></td>
                              			<td>1</td>
                              			<td>31 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/5">Remove</a>&gt;</td>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/3">Going Postal</a></td>
                              			<td>1</td>
                              			<td>28 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/3">Remove</a>&gt;</td>
                              		</tr>
                              	</table>
                              	Total price of all items: 236 EUR
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Pavel, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (0)</a></td>
                              		</tr>
                              	</table>
                              	Your shopping cart is EMPTY.
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Pavel, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (0)</a></td>
                              		</tr>
                              	</table>
                              	Book details:
                              	<h2>The Colour of Magic</h2>
                              	<p style="margin-left: 20px">
                              	Author: Terry Pratchett<br />
                              	Price: 159 EUR<br />
                              	</p>
                              	<h3>&lt;<a href="/ShoppingCart/Add/4">Buy this book</a>&gt;</h3>
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Pavel, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (1)</a></td>
                              		</tr>
                              	</table>
                              	Your shopping cart:
                              	<table>
                              		<tr>
                              			<th>Title</th>
                              			<th>Count</th>
                              			<th>Price</th>
                              			<th>Actions</th>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/4">The Colour of Magic</a></td>
                              			<td>1</td>
                              			<td>159 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/4">Remove</a>&gt;</td>
                              		</tr>
                              	</table>
                              	Total price of all items: 159 EUR
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Pavel, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (1)</a></td>
                              		</tr>
                              	</table>
                              	Our books for you:
                              	<table>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/1">Lord of the Rings</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 59 EUR &lt;<a href="/ShoppingCart/Add/1">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/2">Hobbit: There and Back Again</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 49 EUR &lt;<a href="/ShoppingCart/Add/2">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/3">Going Postal</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 28 EUR &lt;<a href="/ShoppingCart/Add/3">Buy</a>&gt;
                              			</td>
                              		</tr>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/4">The Colour of Magic</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 159 EUR &lt;<a href="/ShoppingCart/Add/4">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/5">I Shall Wear Midnight</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 31 EUR &lt;<a href="/ShoppingCart/Add/5">Buy</a>&gt;
                              			</td>
                              		</tr>
                              	</table>
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              <p>Invalid request.</p>
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (3)</a></td>
                              		</tr>
                              	</table>
                              	Your shopping cart:
                              	<table>
                              		<tr>
                              			<th>Title</th>
                              			<th>Count</th>
                              			<th>Price</th>
                              			<th>Actions</th>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/1">Lord of the Rings</a></td>
                              			<td>2</td>
                              			<td>2 * 59 = 118 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/1">Remove</a>&gt;</td>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/5">I Shall Wear Midnight</a></td>
                              			<td>1</td>
                              			<td>31 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/5">Remove</a>&gt;</td>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/3">Going Postal</a></td>
                              			<td>1</td>
                              			<td>28 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/3">Remove</a>&gt;</td>
                              		</tr>
                              	</table>
                              	Total price of all items: 177 EUR
                              </body>
                              </html>
                              ====
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (2)</a></td>
                              		</tr>
                              	</table>
                              	Your shopping cart:
                              	<table>
                              		<tr>
                              			<th>Title</th>
                              			<th>Count</th>
                              			<th>Price</th>
                              			<th>Actions</th>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/1">Lord of the Rings</a></td>
                              			<td>2</td>
                              			<td>2 * 59 = 118 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/1">Remove</a>&gt;</td>
                              		</tr>
                              		<tr>
                              			<td><a href="/Books/Detail/3">Going Postal</a></td>
                              			<td>1</td>
                              			<td>28 EUR</td>
                              			<td>&lt;<a href="/ShoppingCart/Remove/3">Remove</a>&gt;</td>
                              		</tr>
                              	</table>
                              	Total price of all items: 146 EUR
                              </body>
                              </html>
                              ====
                              
                              """;

        TestProcess(inputRequest, outputWriter);

        string actualHTM = outputWriter.ToString();
        Assert.Equal(expectedHTML, actualHTM);
    }


    [Fact]
    public void CustID_1_EmptyShoppingCart()
    {
        var outputWriter = new StringWriter();

        string inputRequest = """
                               GET 2 http://www.nezarka.net/Books
                               """;

        string expectedHTML = """
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              	<style type="text/css">
                              		table, th, td {
                              			border: 1px solid black;
                              			border-collapse: collapse;
                              		}
                              		table {
                              			margin-bottom: 10px;
                              		}
                              		pre {
                              			line-height: 70%;
                              		}
                              	</style>
                              	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>
                              	Jan, here is your menu:
                              	<table>
                              		<tr>
                              			<td><a href="/Books">Books</a></td>
                              			<td><a href="/ShoppingCart">Cart (2)</a></td>
                              		</tr>
                              	</table>
                              	Our books for you:
                              	<table>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/1">Lord of the Rings</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 59 EUR &lt;<a href="/ShoppingCart/Add/1">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/2">Hobbit: There and Back Again</a><br />
                              				Author: J. R. R. Tolkien<br />
                              				Price: 49 EUR &lt;<a href="/ShoppingCart/Add/2">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/3">Going Postal</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 28 EUR &lt;<a href="/ShoppingCart/Add/3">Buy</a>&gt;
                              			</td>
                              		</tr>
                              		<tr>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/4">The Colour of Magic</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 159 EUR &lt;<a href="/ShoppingCart/Add/4">Buy</a>&gt;
                              			</td>
                              			<td style="padding: 10px;">
                              				<a href="/Books/Detail/5">I Shall Wear Midnight</a><br />
                              				Author: Terry Pratchett<br />
                              				Price: 31 EUR &lt;<a href="/ShoppingCart/Add/5">Buy</a>&gt;
                              			</td>
                              		</tr>
                              	</table>
                              </body>
                              </html>
                              ====

                              """;

        TestProcess(inputRequest, outputWriter);

        string actualHTM = outputWriter.ToString();
        Assert.Equal(expectedHTML, actualHTM);
    }


    [Fact]
    public void InvalidRequest()
    {
        var outputWriter = new StringWriter();

        string inputRequest = """
                               GET 2 http://www.nezarka.net/ShoppingCart/Remove/2
                               """;

        string expectedHTML = """
                              <!DOCTYPE html>
                              <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                              <head>
                              	<meta charset="utf-8" />
                              	<title>Nezarka.net: Online Shopping for Books</title>
                              </head>
                              <body>
                              <p>Invalid request.</p>
                              </body>
                              </html>
                              ====

                              """;

        TestProcess(inputRequest, outputWriter);

        string actualHTM = outputWriter.ToString();
        Assert.Equal(expectedHTML, actualHTM);
    }
}
