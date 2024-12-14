using System;
using System.Data;

namespace InternetoveKnihkupectvi;

internal class Program
{
    static void Main(string[] args)
    {
        var appErrorHandler = new AppErrorHandler(Console.Out);
        appErrorHandler.ExecuteProgram(new Store());
    }
}

public class Store : IProgramCore
{
    public void Run() 
    {
        var store = NezarkaModel.LoadFrom(Console.In);
        var writer = new HtmlToConsoleWriter(Console.Out);

        var handler = new ClientRequestsHandler(store, writer);
        var reader = new ClientRequestsReader(Console.In);

        RequestsProcessing.ProcessRequests(handler, reader);
    }
}