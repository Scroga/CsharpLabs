using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.IO;

namespace InternetoveKnihkupectvi;
public class ClientRequestsReader : IRequestsReader
{
    TextReader _reader;

    public ClientRequestsReader(TextReader reader)
    {
        _reader = reader;
    }

    public Request ReadRequest()
    {
        string? line = _reader.ReadLine();
        if (string.IsNullOrWhiteSpace(line)) 
        {
            return new Request(RequestType.EndOfInput);
        }

        string[] tokens = line.Split(' ');

        try
        {
            if (tokens.Length != 3 || tokens[0] != "GET")
            {
                return new Request(RequestType.Invalid);
            }

            int id = int.Parse(tokens[1]);
            string url = tokens[2];
            return new Request(id, url);

        }
        catch (Exception ex) when (ex is FormatException || ex is IndexOutOfRangeException)
        {
            return new Request(RequestType.Invalid);
        }
    }
}
