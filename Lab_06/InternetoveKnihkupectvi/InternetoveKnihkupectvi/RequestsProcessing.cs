using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetoveKnihkupectvi;

public static class RequestsProcessing
{
    public static void ProcessRequests(IRequestsHandler handler, IRequestsReader reader) 
    {
        var requests = new List<Request>();
        while ( reader.ReadRequest() is { Type: not RequestType.EndOfInput } request ) 
        {
            requests.Add( request );
        }

        foreach (var request in requests)
        {
            handler.ProcessRequest( request );
        }

        handler.Finish();
    }
}
